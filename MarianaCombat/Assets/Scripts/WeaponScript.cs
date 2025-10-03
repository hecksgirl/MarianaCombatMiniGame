using System.Collections;
using UnityEngine;
using System;

public class WeaponScript : MonoBehaviour
{
    public float parryDuration, attackDuration;
    float weaponScaler, weaponEx, minScale;
    public int damage;
    public float orbitDistance;
    public bool isParrying, isAttacking;
    public GameObject player;
    Animator animator;
    KeyCode parryKey;
    KeyCode attackKey;
    KeyCode pauseKey;
    KeyCode AltPauseKey;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = FindFirstObjectByType<PlayerScript>().gameObject;

        parryKey = PlayerInput.Instance.attackKey;
        attackKey = PlayerInput.Instance.parryKey;
        pauseKey = PlayerInput.Instance.pauseKey;
        AltPauseKey = PlayerInput.Instance.AltPauseKey;

        weaponScaler = 0.92f;
        weaponEx = 18.21f;
        minScale = 0.7f;

        isParrying = false;
        isAttacking = false;
    }

    void Update()
    {
        HandleWeaponMovement();

        if (Input.GetKeyDown(attackKey) && Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if ((Input.GetKeyDown(pauseKey) || Input.GetKeyDown(AltPauseKey)) && !Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.None;
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

    void HandleWeaponMovement()
    {
        if (player == null) return;

        //get mouse position in world space
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Camera.main.WorldToScreenPoint(player.transform.position).z;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        //direction from player to mouse
        Vector3 playerPosition = player.transform.position;
        Vector3 direction = (mouseWorldPosition - playerPosition).normalized;

        //weapon orbiting
        transform.position = playerPosition + direction * orbitDistance;

        //rotate weapon so it faces the mouse- doesn't work
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90); // adjust +90 for sprite art
    }

}
