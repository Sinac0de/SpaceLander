using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour {

    private const float GRAVITY_NORMAL = 0.7f;

    public static Lander Instance { get; private set; }


    public event EventHandler OnUpForce;
    public event EventHandler OnLeftForce;
    public event EventHandler OnRightForce;
    public event EventHandler OnBeforeForce;
    public event EventHandler OnCoinPickup;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs {
        public State state;
    }

    public event EventHandler<OnLandedEventArgs> OnLanded;
    public class OnLandedEventArgs : EventArgs {
        public LandingType landingType;
        public float landingSpeed;
        public float landingAngle;
        public float multiplier;
        public int score;
    }


    public enum LandingType {
        Successful,
        TooSteep,
        TooHard,
        Crashed,
    }


    public enum State {
        WaitingToStart,
        Normal,
        GameOver
    }




    private Rigidbody2D landerRigidbody2D;

    private State state;

    private float fuelAmount;
    private float maxFuelAmount = 10f;


    private void Awake() {
        Instance = this;

        state = State.WaitingToStart;

        landerRigidbody2D = GetComponent<Rigidbody2D>();
        fuelAmount = maxFuelAmount;
    }


    private void FixedUpdate() {

        switch (state) {
            case State.WaitingToStart:
                landerRigidbody2D.gravityScale = 0f;
                if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.leftArrowKey.isPressed || Keyboard.current.rightArrowKey.isPressed) {
                    SetState(State.Normal);
                }
                break;
            case State.Normal:
                landerRigidbody2D.gravityScale = GRAVITY_NORMAL;

                OnBeforeForce?.Invoke(this, EventArgs.Empty);

                if (fuelAmount <= 0) {
                    return;
                }

                if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.leftArrowKey.isPressed || Keyboard.current.rightArrowKey.isPressed) {
                    ConsumeFuel();
                }
                //isPressed: holding the button
                if (Keyboard.current.upArrowKey.isPressed) {
                    float force = 700f;
                    landerRigidbody2D.AddForce(force * transform.up * Time.deltaTime);
                    OnUpForce?.Invoke(this, EventArgs.Empty);
                }

                if (Keyboard.current.leftArrowKey.isPressed) {
                    float turnSpeed = 100f;
                    landerRigidbody2D.AddTorque(turnSpeed * Time.deltaTime);
                    OnLeftForce?.Invoke(this, EventArgs.Empty);
                }

                if (Keyboard.current.rightArrowKey.isPressed) {
                    float turnSpeed = -100f;
                    landerRigidbody2D.AddTorque(turnSpeed * Time.deltaTime);
                    OnRightForce?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }




    }


    private void OnCollisionEnter2D(Collision2D collision) {
        if (!collision.gameObject.TryGetComponent(out LandingPad landingPad)) {
            //Crashed! Landed on Terrain.
            OnLanded?.Invoke(this, new OnLandedEventArgs {
                landingType = LandingType.Crashed,
                landingSpeed = 0,
                landingAngle = 0,
                multiplier = 0,
                score = 0
            });
            SetState(State.GameOver);

            return;
        }


        float relativeVelocityMagnitude = collision.relativeVelocity.magnitude;
        float safeLandingVelocity = 3.5f;

        if (relativeVelocityMagnitude > safeLandingVelocity) {
            //Landed Too Hard!
            OnLanded?.Invoke(this, new OnLandedEventArgs {
                landingType = LandingType.TooHard,
                landingSpeed = relativeVelocityMagnitude,
                landingAngle = 0,
                multiplier = 0,
                score = 0
            });
            SetState(State.GameOver);
            return;
        }

        float dotVector = Vector2.Dot(Vector2.up, transform.up);
        float minDotVector = 0.9f;

        if (dotVector < minDotVector) {
            //Landed at a Bad Angle!
            OnLanded?.Invoke(this, new OnLandedEventArgs {
                landingType = LandingType.TooSteep,
                landingSpeed = relativeVelocityMagnitude,
                landingAngle = dotVector,
                multiplier = 0,
                score = 0
            });
            SetState(State.GameOver);
            return;
        }

        float maxScoreAmountLandingAngle = 100f;
        float scoreDotVectorMultiplier = 10f;

        float landingAngleScore = maxScoreAmountLandingAngle - Mathf.Abs(dotVector - 1f) * scoreDotVectorMultiplier * maxScoreAmountLandingAngle;


        float maxScoreAmountLandingSpeed = 100f;
        float landingSpeedScore = (safeLandingVelocity - relativeVelocityMagnitude) * maxScoreAmountLandingSpeed;


        int score = Mathf.RoundToInt((landingAngleScore + landingSpeedScore) * landingPad.GetScoreMultiplier());

        OnLanded?.Invoke(this, new OnLandedEventArgs {
            landingType = LandingType.Successful,
            landingSpeed = relativeVelocityMagnitude,
            landingAngle = dotVector,
            multiplier = landingPad.GetScoreMultiplier(),
            score = score
        });
        SetState(State.GameOver);

    }


    private void OnTriggerEnter2D(Collider2D collider2D) {

        if (collider2D.gameObject.TryGetComponent(out FuelPickup fuelPickup)) {
            float fuelRefillAmount = 10f;
            fuelAmount += fuelRefillAmount;

            if (fuelAmount > maxFuelAmount) {
                fuelAmount = maxFuelAmount;
            }

            fuelPickup.DestroySelf();
        }

        if (collider2D.gameObject.TryGetComponent(out CoinPickup coinPickup)) {
            OnCoinPickup?.Invoke(this, EventArgs.Empty);
            coinPickup.DestroySelf();
        }

    }

    private void SetState(State state) {
        this.state = state;
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
            state = state
        });
    }


    private void ConsumeFuel() {
        float fuelConsumptionAmount = 1f;
        fuelAmount -= fuelConsumptionAmount * Time.deltaTime;
    }

    public float GetSpeedX() {
        return landerRigidbody2D.linearVelocityX;
    }

    public float GetSpeedY() {
        return landerRigidbody2D.linearVelocityY;
    }

    public float GetFuelAmountNormalized() {
        return fuelAmount / maxFuelAmount;
    }
}
