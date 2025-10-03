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
        if (collision.GetComponent<WeaponScript>() && collision.GetComponent<WeaponScript>().isAttacking 
            && canTakeDamage)
        {
            TakeDamage();
        }
        if (collision.gameObject.tag == "Player" && player.canTakeDamage)
        {
            DealDamage(damage);
            StartCoroutine(player.DamageCooldown());
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
        Debug.Log("Health: " + player.currentHealth);
        GameManager.Instance.UpdateScore(-1);
        if (player.currentHealth <= 0)
        {
            print("Game Over");
        }
    }
}
