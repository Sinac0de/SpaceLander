using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour {


    public event EventHandler OnUpForce;
    public event EventHandler OnLeftForce;
    public event EventHandler OnRightForce;
    public event EventHandler OnBeforeForce;

    private Rigidbody2D landerRigidbody2D;

    private float fuelAmount = 10f;


    private void Awake() {
        landerRigidbody2D = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate() {
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
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        if (!collision.gameObject.TryGetComponent(out LandingPad landingPad)) {
            //Crashed! Landed on Terrain.
            Debug.Log("Crashed! Landed on Terrain.");
            return;
        }


        float relativeVelocityMagnitude = collision.relativeVelocity.magnitude;
        float safeLandingVelocity = 3.5f;

        if (relativeVelocityMagnitude > safeLandingVelocity) {
            //Landed Too Hard!
            Debug.Log("Landed Too Hard!");
            return;
        }

        float dotVector = Vector2.Dot(Vector2.up, transform.up);
        float minDotVector = 0.9f;

        if (dotVector < minDotVector) {
            //Landed at a Bad Angle!
            Debug.Log("Landed at a Bad Angle!");
            return;
        }

        float maxScoreAmountLandingAngle = 100f;
        float scoreDotVectorMultiplier = 10f;

        float landingAngleScore = maxScoreAmountLandingAngle - Mathf.Abs(dotVector - 1f) * scoreDotVectorMultiplier * maxScoreAmountLandingAngle;


        float maxScoreAmountLandingSpeed = 100f;
        float landingSpeedScore = (safeLandingVelocity - relativeVelocityMagnitude) * maxScoreAmountLandingSpeed;


        Debug.Log("Landed Successfully!");
        Debug.Log("landingAngleScore: " + landingAngleScore);
        Debug.Log("landingSpeedScore: " + landingSpeedScore);

        int score = Mathf.RoundToInt((landingAngleScore + landingSpeedScore) * landingPad.GetScoreMultiplier());
        Debug.Log("Final Score: " + score);
    }


    private void OnTriggerEnter2D(Collider2D collider2D) {

        if (collider2D.gameObject.TryGetComponent(out FuelPickup fuelPickup)) {
            float fuelRefillAmount = 10f;
            fuelAmount += fuelRefillAmount;
            fuelPickup.DestroySelf();
        }

    }


    private void ConsumeFuel() {
        Debug.Log("fuel: " + fuelAmount);
        float fuelConsumptionAmount = 1f;
        fuelAmount -= fuelConsumptionAmount * Time.deltaTime;
    }
}
