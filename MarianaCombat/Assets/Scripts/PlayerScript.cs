using System.Collections;
using UnityEngine;
using System;

public class PlayerScript : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth, playerTakeDamageCooldownSeconds;
    public GameObject weapon;
    public bool canTakeDamage;
    public PlayerHealthBar healthBar;

    void Start()
    {
        healthBar = FindFirstObjectByType<PlayerHealthBar>();
        currentHealth = maxHealth;
        canTakeDamage = true;
    }

    public IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(playerTakeDamageCooldownSeconds);
        canTakeDamage = true;
    }
}
