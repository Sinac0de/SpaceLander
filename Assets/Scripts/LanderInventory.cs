using System;
using System.Collections.Generic;
using UnityEngine;

public class LanderInventory : MonoBehaviour
{

    public event EventHandler OnInventoryChanged;

    private List<KeyDataSO> heldKeys = new List<KeyDataSO>();

    public void AddKey(KeyDataSO keyData) {
        heldKeys.Add(keyData);
        OnInventoryChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool HasKey(KeyDataSO keyData) {
        return heldKeys.Contains(keyData);
    }


    public void RemoveKey(KeyDataSO keyData) {
        if (HasKey(keyData)) {
            heldKeys.Remove(keyData);
            OnInventoryChanged?.Invoke(this, EventArgs.Empty);
        }
    }



}
