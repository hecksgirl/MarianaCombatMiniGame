using System.Collections;
using UnityEngine;
using System;

public class PlayerScript : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth, playerTakeDamageCooldownSeconds;
    public float parryDuration, attackDuration;
    public GameObject weapon;
    bool isParrying, isAttacking;
    public bool canTakeDamage;

    void Start()
    {
        currentHealth = maxHealth;
        canTakeDamage = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isParrying)
        {
            StartCoroutine(Parry());
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Parry()
    {
        isParrying = true;
        yield return new WaitForSeconds(parryDuration);
        isParrying = false;
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
    }

    public IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(playerTakeDamageCooldownSeconds);
        canTakeDamage = true;
    }
}
