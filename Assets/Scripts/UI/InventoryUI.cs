
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {
    [SerializeField] private Transform keyContainer;
    [SerializeField] private GameObject keyIconPrefab; 

    private void Start() {
        LanderInventory.Instance.OnInventoryChanged += LanderInventory_OnInventoryChanged;
        UpdateVisuals();
    }

    private void LanderInventory_OnInventoryChanged(object sender, System.EventArgs e) {
        UpdateVisuals();
    }

    private void UpdateVisuals() {
        //Delete all keys
        foreach (Transform child in keyContainer) {
            Destroy(child.gameObject);
        }


        // generate icon for each key
        foreach (KeyDataSO key in LanderInventory.Instance.GetHeldKeys()) {
            GameObject iconTransform = Instantiate(keyIconPrefab, keyContainer);

            Image iconImage = iconTransform.GetComponent<Image>();
            iconImage.sprite = key.HUDSprite;
        }
    }
}