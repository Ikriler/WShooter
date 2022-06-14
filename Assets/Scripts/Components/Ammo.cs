using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField]
    public int _maxAmmoInMagazie;
    [SerializeField]
    public int _maxAmmoCount;

    private int _curentAmmoInMagazine;
    private int _currentMaxAmmo;

    private void Awake()
    {
        _curentAmmoInMagazine = _maxAmmoInMagazie;
        _currentMaxAmmo = _maxAmmoCount;
    }

    public void AmmoDown(int i = 1)
    {
        if(_curentAmmoInMagazine != 0)
        {
            _curentAmmoInMagazine -= i;
        }
    }

    public int GetCurrentAmmoCountInMagazine()
    {
        return _curentAmmoInMagazine;
    }

    public int GetCurrentMaxAmmoCount()
    {
        return _currentMaxAmmo;
    }

    public void Reload()
    {
        int needAmmo = _maxAmmoInMagazie - _curentAmmoInMagazine;
        if (needAmmo > _currentMaxAmmo)
        {
            _curentAmmoInMagazine += _currentMaxAmmo;
            _currentMaxAmmo = 0;
            return;
        }
        if(needAmmo <= _currentMaxAmmo)
        {
            _curentAmmoInMagazine += needAmmo;
            _currentMaxAmmo -= needAmmo;
            return;
        }
    }

    public void FillAmmo()
    {
        _curentAmmoInMagazine = _maxAmmoInMagazie;
        _currentMaxAmmo = _maxAmmoCount;
    }
}
