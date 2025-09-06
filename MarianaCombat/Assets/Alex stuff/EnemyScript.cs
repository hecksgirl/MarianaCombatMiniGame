using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    int health;
    public int maxHealth, damage;

    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("weapon"))
        {
            health -= collision.gameObject.GetComponent<WeaponScript>().damage;
        }
    }
}
