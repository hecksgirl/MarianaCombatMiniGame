using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    private Slider slider;
    public float staminaCost, stamina, maxStamina, staminaGainPerSecond;

    void Start()
    {
        slider = GetComponent<Slider>();
        stamina = maxStamina;
    }

    void Update()
    {
        IncreaseStamina();
        slider.value = stamina/maxStamina;
    }

    public void DecreaseStamina()
    {
        stamina -= staminaCost;
    }

    void IncreaseStamina()
    {
        stamina += staminaGainPerSecond * Time.deltaTime;
    }
}
