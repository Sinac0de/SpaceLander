using UnityEngine;

public class DoubleDoor : MonoBehaviour {
    [SerializeField] private KeyDataSO requiredKey;

    [Header("Door Visuals")]
    [SerializeField] private Transform leftDoorVisual;
    [SerializeField] private Transform rightDoorVisual;

    [Header("Settings")]
    [SerializeField] private float slideDistance = 1.75f; 
    [SerializeField] private float openSpeed = 5f;

    private bool isOpening = false;
    private Vector3 leftClosedPosition;
    private Vector3 rightClosedPosition;
    private Vector3 leftOpenPosition;
    private Vector3 rightOpenPosition;

    private void Awake() {
        leftClosedPosition = leftDoorVisual.localPosition;
        rightClosedPosition = rightDoorVisual.localPosition;

       
        leftOpenPosition = leftClosedPosition + (Vector3.left * slideDistance);
        rightOpenPosition = rightClosedPosition + (Vector3.right * slideDistance);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.TryGetComponent(out Lander lander)) {
            if (lander.TryGetComponent(out LanderInventory inventory)) {

                if (inventory.HasKey(requiredKey)) {
                    isOpening = true;
                    LanderInventory.Instance.RemoveKey(requiredKey);
                }
            }
        }
    }

    private void Update() {
        if (isOpening) {
            leftDoorVisual.localPosition = Vector3.Lerp(leftDoorVisual.localPosition, leftOpenPosition, Time.deltaTime * openSpeed);
            rightDoorVisual.localPosition = Vector3.Lerp(rightDoorVisual.localPosition, rightOpenPosition, Time.deltaTime * openSpeed);


            if (Vector3.Distance(leftDoorVisual.localPosition, leftOpenPosition) < 0.01f) {
                leftDoorVisual.localPosition = leftOpenPosition;
                rightDoorVisual.localPosition = rightOpenPosition;
                isOpening = false;

                if (TryGetComponent(out Collider2D doorCollider)) {
                    doorCollider.enabled = false;
                }
            }
        }
    }
}