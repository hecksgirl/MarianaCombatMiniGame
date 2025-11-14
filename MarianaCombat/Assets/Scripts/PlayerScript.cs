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

    public void DamCooldown()
    { 
        StartCoroutine(DamageCooldown());
    }


    public IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        Debug.Log("Player damage cooldown started.");
        yield return new WaitForSeconds(playerTakeDamageCooldownSeconds);
        canTakeDamage = true;
        Debug.Log("Player can take damage again.");
    }
}
