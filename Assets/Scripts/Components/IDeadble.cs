using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDeadble : MonoBehaviour
{
    private Health currentHealth;
    private void Awake()
    {
        currentHealth = GetComponent<Health>();
    }
    void Update()
    {
        if (currentHealth._health <= 0) Destroy(gameObject); 
    }
}
