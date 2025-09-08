using System.Collections;
using UnityEngine;
using System;

public class PlayerScript : MonoBehaviour
{
    int currentHealth;
    public int maxHealth, playerTakeDamageCooldownSeconds;
    public float parryDuration, attackDuration;
    public GameObject weapon;
    bool canTakeDamage, isParrying, isAttacking;

    void Start()
    {
        currentHealth = maxHealth;
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

    void PlayerTakesDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            print("Game Over");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && canTakeDamage)
        {
            PlayerTakesDamage(collision.gameObject.GetComponent<EnemyScript>().damage);
            StartCoroutine(DamageCooldown());
        }
    }

    IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(playerTakeDamageCooldownSeconds);
        canTakeDamage = true;
    }
}
