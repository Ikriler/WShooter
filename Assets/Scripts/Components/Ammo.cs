using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField]
    public int _ammoCount;

    private int maxAmmoCount;

    private void Awake()
    {
        maxAmmoCount = _ammoCount;
    }

    public void AmmoDown(int ammoCount)
    {
        if (_ammoCount + ammoCount > maxAmmoCount)
        {
            _ammoCount = maxAmmoCount;
        }
        else
        {
            _ammoCount += ammoCount;
        }
    }

    public void AmmoUp(int ammoCount = 1)
    {
        if (_ammoCount - ammoCount < 0)
        {
            _ammoCount = 0;
        }
        else
        {
            _ammoCount -= ammoCount;
        }
    }

    public int GetMaxHealth()
    {
        return maxAmmoCount;
    }
}
