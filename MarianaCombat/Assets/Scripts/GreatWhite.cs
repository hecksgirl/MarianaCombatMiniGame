using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatWhite : MonoBehaviour
{
    public Sprite silhouette, attackSprite;
    public bool isAttacking, attackSuccessful;
    public int graceSeconds, damage;

    bool canAttack;
    SpriteRenderer spriteRenderer;
    Animator animator;
    PlayerScript player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<PlayerScript>();
        isAttacking = false;
        animator = GetComponent<Animator>();
        animator.SetTrigger("Appear");
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = silhouette;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !isAttacking)
        {
            Attack();
        }
    }

    void Attack()
    {
        spriteRenderer.sprite = attackSprite;
        animator.SetTrigger("Attack");
        Debug.Log("Great White Attacking");
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        attackSuccessful = true;

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + graceSeconds);
        
        isAttacking = false;

        if(attackSuccessful)
        {
            Debug.Log("Great White attack hit the player");
            player.currentHealth -= damage;
        }
        else
        {
            Debug.Log("Great White attack was parried");
        }

        Destroy(gameObject);
    }
}
