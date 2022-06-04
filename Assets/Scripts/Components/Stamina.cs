using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField]
    public float _stamina = 100;
    [SerializeField]
    public float _staminaPerSecond = 10;

    private float maxStamina;
    private bool staminaTumble = false;

    private void Awake()
    {
        maxStamina = _stamina;
        StartCoroutine(StaminaCollector());
    }

    public void StaminaUp(float staminaCount)
    {
        if (_stamina + staminaCount > maxStamina)
        {
            _stamina = maxStamina;
        }
        else
        {
            _stamina += staminaCount;
        }
    }

    public void StaminaDown(float staminaCount)
    {
        if (_stamina - staminaCount < 0)
        {
            _stamina = 0;
        }
        else
        {
            _stamina -= staminaCount;
        }
        staminaTumble = true;
    }

    public float GetMaxStamina()
    {
        return maxStamina;
    }

    private IEnumerator StaminaCollector()
    {
        while(true)
        {
            if (staminaTumble)
            {
                staminaTumble = false;
                yield return new WaitForSeconds(1);
            }
            StaminaUp(_staminaPerSecond);
            yield return new WaitForSeconds(1);
        }
    }
}
