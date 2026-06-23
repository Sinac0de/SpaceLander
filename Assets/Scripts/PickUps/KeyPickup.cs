using UnityEngine;
using UnityEngine.UI;

public class KeyPickup : MonoBehaviour, IInteractable {

    [SerializeField] private KeyDataSO keydataSO;
    [SerializeField] private float interactionDuration = 2.5f;

    [SerializeField] private SpriteRenderer iconSpriteRenderer;
    [SerializeField] private Image spinnerFillImage;

    private float timer = 0f;
    private bool isCollected = false;

    private void Start() {
        if (keydataSO != null) {
            if (iconSpriteRenderer != null) {
                iconSpriteRenderer.sprite = keydataSO.sprite;
            }
            if (spinnerFillImage != null) {
                spinnerFillImage.color = keydataSO.color;
                spinnerFillImage.fillAmount = 0f;
            }
        }
    }


    private void CollectKey(Lander lander) {
        isCollected = true;
        if(lander.TryGetComponent(out LanderInventory landerInventory)) {
            landerInventory.AddKey(keydataSO);
        }

        DestroySelf();
    }


    public void OnHover(Lander lander, float deltaTime) {
        if (isCollected) {
            return;
        }

        timer += deltaTime;


        spinnerFillImage.fillAmount = timer / interactionDuration;

        if(timer >= interactionDuration) {
            CollectKey(lander);
        }
    }


    public void CancelHover() {
        if (isCollected) {
            return;
        }

        spinnerFillImage.fillAmount = 0f;
        timer = 0f;
    }

    public void DestroySelf() {
        Destroy(gameObject);
    }




}
