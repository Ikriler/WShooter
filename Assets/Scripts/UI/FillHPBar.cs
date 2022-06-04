using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillHPBar : MonoBehaviour
{
    [SerializeField]
    public float _healthSmooth = 0.9f;

    private GameObject HPBar;
    private Health playerHealth;
    private void Awake()
    {
        HPBar = GameObject.FindGameObjectWithTag("HPBar");
        playerHealth = GetComponent<Health>();
        if (HPBar == null)
        {
            Debug.Log("HPBar not found");
        }
        if(playerHealth == null)
        {
            Debug.Log("Health not found");
        }
    }
    void Update()
    {
        if (HPBar == null) return;
        if (playerHealth == null) return;
        HPBar.GetComponent<Image>().fillAmount = Mathf.Lerp(HPBar.GetComponent<Image>().fillAmount, playerHealth._health / playerHealth.GetMaxHealth(), _healthSmooth * Time.fixedDeltaTime);
    }
}
