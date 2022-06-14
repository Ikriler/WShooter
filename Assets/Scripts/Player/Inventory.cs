using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject[] weapons = new GameObject[6];

    private int currentWeapon = 0;

    private void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if(currentWeapon >= weapons.Length - 1)
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (currentWeapon <= 0)
            {
                currentWeapon = weapons.Length - 1;
            }
            else
            {
                currentWeapon--;
            }
        }
        ChangeWeapon(currentWeapon);
    }

    private void ChangeWeapon(int weaponIndex)
    {
        if(weapons[weaponIndex] == null)
        {
            currentWeapon = currentWeapon == weapons.Length - 1 ? 0 : currentWeapon++;
            return;
        }
        if (weapons[weaponIndex].activeSelf) return;
        DisableWeapon();
        weapons[weaponIndex].SetActive(true);
    }

    private void DisableWeapon()
    {
        foreach(GameObject weapon in weapons)
        {
            if(weapon != null) weapon.SetActive(false);
        }
    }

    public void ReloadGun(int gunIndex)
    {
        weapons[gunIndex].GetComponent<Ammo>().FillAmmo();
    }
}
