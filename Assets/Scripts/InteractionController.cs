using UnityEngine;

public class InteractionController : MonoBehaviour
{
    private IInteractable currentInteractable;
    private Lander lander;

    private void Awake() {
        lander = GetComponent<Lander>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.TryGetComponent(out IInteractable interactable)) {
            currentInteractable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.TryGetComponent(out IInteractable interactable) && currentInteractable == interactable) {
            currentInteractable.CancelHover();
            currentInteractable = null;
        }
    }

    private void Update() {
        if(currentInteractable != null) {
            currentInteractable.OnHover(lander, Time.deltaTime);
        }
    }
}
