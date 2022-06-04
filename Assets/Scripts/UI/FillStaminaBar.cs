using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillStaminaBar : MonoBehaviour
{
    private GameObject StaminaBar;
    private Stamina playerStamina;
    private void Awake()
    {
        StaminaBar = GameObject.FindGameObjectWithTag("StaminaBar");
        playerStamina = GetComponent<Stamina>();
        if (StaminaBar == null)
        {
            Debug.Log("StaminaBar not found");
        }
        if (playerStamina == null)
        {
            Debug.Log("Stamina not found");
        }
    }
    void Update()
    {
        if (StaminaBar == null) return;
        if (playerStamina == null) return;
        StaminaBar.GetComponent<Image>().fillAmount = playerStamina._stamina / playerStamina.GetMaxStamina();
    }
}
