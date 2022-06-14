using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    public int _gunIndex;

    private void OnTriggerEnter(Collider other)
    {
        Inventory inventory = other.GetComponent<Inventory>();
        if (inventory == null) return;
        if (inventory.GunIsFull(_gunIndex)) return;
        inventory.ReloadGun(_gunIndex);
        Destroy(gameObject);
    }
}
