using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    int health;
    public int maxHealth, damage, damageCooldown;
    bool canTakeDamage, isTouchingPlayer;
    public PlayerScript player;
    public float moveSpeed;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<PlayerScript>();
        health = maxHealth;
        canTakeDamage = true;
        rb = GetComponent<Rigidbody2D>();
        isTouchingPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTouchingPlayer)
            MoveEnemy();
    }

    public void TakeDamage()
    {
        EnemyManager.Instance.enemies.Remove(this);
        GameManager.Instance.UpdateScore(1);
        EnemyManager.Instance.SpawnEnemy(gameObject);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerScript>())
            isTouchingPlayer = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        WeaponScript weapon = collision.GetComponent<WeaponScript>();

        if (weapon != null && weapon.isAttacking && canTakeDamage)
        {
            TakeDamage();
        }
        if (collision.gameObject.CompareTag("Player") && player.canTakeDamage)
        {
            DealDamage(damage);
            player.DamCooldown();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerScript>())
            isTouchingPlayer = false;
    }

    private void MoveEnemy()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;

        rb.AddForce(direction * moveSpeed);
    }

    void DealDamage(int damage)
    {
        player.currentHealth -= damage;
        player.healthBar.UpdateHealthBar(player.currentHealth/(float)player.maxHealth);
        Debug.Log("PlayerHealth: " + player.currentHealth);

        if (player.currentHealth <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }
}
