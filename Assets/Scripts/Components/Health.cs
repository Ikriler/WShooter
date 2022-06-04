using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    public float _health;

    private float maxHealth;

    private void Awake()
    {
        maxHealth = _health;
    }

    public void HealthUp(float healthCount)
    {
        if(_health + healthCount > maxHealth)
        {
            _health = maxHealth;
        }
        else
        {
            _health += healthCount;
        }
    }

    public void HealthDown(float healthCount)
    {
        if(_health - healthCount < 0)
        {
            _health = 0;
        }
        else
        {
            _health -= healthCount;
        }
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }
}
