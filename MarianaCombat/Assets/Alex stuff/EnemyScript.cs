using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    int health;
    public int maxHealth, damage, damageCooldown;
    bool canTakeDamage;
    public PlayerScript player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<PlayerScript>();
        health = maxHealth;
        canTakeDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log("Enemy took " + damage + " damage. Remaining health: " + health);

        if (health <= 0)
        {
            Debug.Log("Enemy defeated!");
            Destroy(gameObject);
        }

        StartCoroutine(DamageCooldown());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enemy colliding with " + collision.gameObject.name);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<WeaponScript>() && canTakeDamage)
        {
            Debug.Log("Enemy hit by weapon for " + collision.GetComponent<WeaponScript>().damage + "damage");
            TakeDamage(collision.gameObject.GetComponent<WeaponScript>().damage);
        }
    }

    IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }
}
