using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKitPickup : MonoBehaviour
{
    [SerializeField]
    public float _extraHealth = 30;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null && other.GetComponent<Health>() != null)
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth._health == playerHealth.GetMaxHealth()) return;

            playerHealth.HealthUp(_extraHealth);

            Destruction();
        }
    }

    private void Destruction()
    {
        Destroy(gameObject);
    }
}
