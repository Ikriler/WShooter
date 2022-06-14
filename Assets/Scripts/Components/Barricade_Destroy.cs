using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade_Destroy : MonoBehaviour
{
    public GameObject _barricade_Full;
    public GameObject _barricade_Destroyed;

    private Health _barricadeHealh;
    void Awake()
    {
        _barricadeHealh = GetComponent<Health>();
    }

    void Update()
    {
        if(_barricadeHealh._health <= 0)
        {
            if(_barricade_Full.activeSelf)
            {
                _barricade_Full.SetActive(false);
                _barricade_Destroyed.SetActive(true);
            }
        }
    }
}
