using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    private Slider slider;
    public Image fill;
    public Color defaultSliderColor;
    public float staminaCost, stamina, maxStamina, staminaGainPerSecond;

    void Start()
    {
        slider = GetComponent<Slider>();
        stamina = maxStamina;
        fill.color = defaultSliderColor;
    }

    void Update()
    {
        IncreaseStamina();
        HandleSliderValues();
    }

    public void DecreaseStamina()
    {
        stamina -= staminaCost;
    }

    void IncreaseStamina()
    {
        stamina += staminaGainPerSecond * Time.deltaTime;
        Mathf.Clamp(stamina, -staminaCost, maxStamina);
    }

    void HandleSliderValues()
    {
        slider.value = stamina / maxStamina;
        fill.color = Color.Lerp(Color.red, defaultSliderColor, stamina / maxStamina);
        Debug.Log(fill.color);
    }
}
