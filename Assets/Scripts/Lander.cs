using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour {

    private Rigidbody2D landerRigidbody2D;


    private void Awake() {
        landerRigidbody2D = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate() {
        if (Keyboard.current.upArrowKey.isPressed) {
            float force = 700f;
            landerRigidbody2D.AddForce(force * transform.up * Time.deltaTime);
        }

        if (Keyboard.current.leftArrowKey.isPressed) {
            float turnSpeed = 100f;
            landerRigidbody2D.AddTorque(turnSpeed * Time.deltaTime);
        }

        if (Keyboard.current.rightArrowKey.isPressed) {
            float turnSpeed = -100f;
            landerRigidbody2D.AddTorque(turnSpeed * Time.deltaTime);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision) {
        float safeLandingVelocity = 3.5f;
        
        if (collision.relativeVelocity.magnitude > safeLandingVelocity) {
            //Landed Too Hard!
            Debug.Log("Landed Too Hard!");
            return;
        }

        float dotVector = Vector2.Dot(Vector2.up, transform.up);
        float minDotVector = 0.9f;

        if(dotVector < minDotVector) {
            //Landed at a Bad Angle!
            Debug.Log("Landed at a Bad Angle!");
            return;
        }


        Debug.Log("Landed Successfully!");
    }
}
