using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    private Image fill;
    public Color defaultSliderColor;
    public float staminaCost, stamina, maxStamina, staminaGainPerSecond;

    void Start()
    {
        fill = GetComponent<Image>();
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
        Debug.Log("Stamina Decreased");
    }

    void IncreaseStamina()
    {
        if(stamina < maxStamina)
            stamina += staminaGainPerSecond * Time.deltaTime;
    }

    void HandleSliderValues()
    {
        fill.fillAmount = stamina / maxStamina;
        fill.color = Color.Lerp(Color.red, defaultSliderColor, stamina / maxStamina);
        Debug.Log(fill.color);
    }
}
