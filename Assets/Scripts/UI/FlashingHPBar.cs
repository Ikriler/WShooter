using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingHPBar : MonoBehaviour
{
    [SerializeField]
    public float _smoothFlashing = 0.7f;

    private Image HPBarImage;
    private bool flashingCoroutineState = false;
    private void Awake()
    {
        HPBarImage = GetComponent<Image>();
    }

    void Update()
    {
        if(!flashingCoroutineState && HPBarImage.fillAmount < 0.33f)
        {
            flashingCoroutineState = true;
            StartCoroutine(Flashing());
        }
    }

    public IEnumerator Flashing()
    {
        Color newColor = HPBarImage.color;
        newColor.a = 0;
        for(; HPBarImage.color.a > 0.1f;)
        {
            HPBarImage.color = Color.Lerp(HPBarImage.color, newColor, _smoothFlashing * Time.fixedDeltaTime);
            yield return new WaitForSeconds(0.09f);
        }
        newColor.a = 1;
        for (; HPBarImage.color.a < 0.9f;)
        {
            HPBarImage.color = Color.Lerp(HPBarImage.color, newColor, _smoothFlashing * Time.fixedDeltaTime);
            yield return new WaitForSeconds(0.09f);
        }
        flashingCoroutineState = false;
    }
}
