using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
    }
    public void UpdateHealthBar(float healthPercentage)
    {
        healthBar.fillAmount = healthPercentage;
    }
}
