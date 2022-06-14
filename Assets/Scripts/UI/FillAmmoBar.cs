using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillAmmoBar : MonoBehaviour
{
    private Text ammoBar;
    private Ammo ammo;
    private WeaponController weaponController;
    private void Awake()
    {
        ammoBar = GameObject.FindGameObjectWithTag("AmmoBar").GetComponent<Text>();
        ammo = GetComponent<Ammo>();
        weaponController = GetComponent<WeaponController>();
    }

    void Update()
    {
        string maxAmmo = weaponController != null && weaponController._gunIsInfinity ? "INF" : ammo.GetCurrentMaxAmmoCount().ToString();

        ammoBar.text = ammo.GetCurrentAmmoCountInMagazine() + " / " + maxAmmo;
    }
}
