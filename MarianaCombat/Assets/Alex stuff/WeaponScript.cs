using System.Collections;
using UnityEngine;
using System;

public class WeaponScript : MonoBehaviour
{
    public float parryDuration, attackDuration;
    float weaponScaler, weaponEx, minScale;
    public int damage;
    int orbitDistance;
    public bool isParrying, isAttacking;
    public GameObject player;
    Animator animator;

    private void Start()
    {
        weaponScaler = 0.92f;
        weaponEx = 18.21f;
        minScale = 0.7f;
        orbitDistance = 1;

        isParrying = false;
        isAttacking = false;

        animator = GetComponent<Animator>();
        player = FindFirstObjectByType<PlayerScript>().gameObject;
        ZeroMousePosition();
    }

    void Update()
    {
        HandleWeaponMovement();

        if (Input.GetKeyDown(KeyCode.Mouse0) && Cursor.visible)
        {
            Cursor.visible = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Cursor.visible)
        {
            Cursor.visible = true;
        }

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
        Debug.Log("Attack");
        isAttacking = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
    }

    void ZeroMousePosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Camera.main.WorldToScreenPoint(player.transform.position).z;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.x = 0;
        mouseWorldPosition.y = 0;
        mouseWorldPosition.z = 0;
    }

    void HandleWeaponMovement()
    {
        if (player == null) return;

        // Get mouse position in world space
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Camera.main.WorldToScreenPoint(player.transform.position).z;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        // Direction from player to mouse
        Vector3 playerPosition = player.transform.position;
        Vector3 direction = (mouseWorldPosition - playerPosition).normalized;

        // Keep weapon orbiting at fixed distance
        transform.position = playerPosition + direction * orbitDistance;

        // Scale is fixed now, so just clamp it to minScale
        transform.localScale = Vector3.one * minScale;

        // Rotate weapon so it faces the mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90); // adjust +90 for sprite art
    }

}
