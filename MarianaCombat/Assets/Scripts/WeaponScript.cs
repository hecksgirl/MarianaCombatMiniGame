using System.Collections;
using UnityEngine;
using System;

public class WeaponScript : MonoBehaviour
{
    public float parryDuration, attackDuration;
    public int damage;
    public float orbitDistance;
    public bool isParrying, isAttacking;
    public GameObject player;
    Animator animator;
    public KeyCode pauseKey, AltPauseKey, UpAttack, DownAttack, LeftAttack, RightAttack, parryKey, UpLeftAttack, UpRightAttack, DownLeftAttack, DownRightAttack;
    public StaminaBar staminaBar;

    private enum AttackDirection
    {
        up, down, left, right, upLeft, upRight, downLeft, downRight, neutral
    }

    AttackDirection attackDirection;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = FindFirstObjectByType<PlayerScript>().gameObject;

        pauseKey = PlayerInput.Instance.pauseKey;
        AltPauseKey = PlayerInput.Instance.AltPauseKey;

        isParrying = false;
        isAttacking = false;

        staminaBar = FindFirstObjectByType<StaminaBar>();
    }

    void Update()
    {
        attackDirection = AttackDirection.neutral;

        if (Input.GetKey(UpAttack))
        {
            Debug.Log("up attack");
            attackDirection = AttackDirection.up;
        }
        else if (Input.GetKey(DownAttack))
            attackDirection = AttackDirection.down;
        else if (Input.GetKey(LeftAttack))
            attackDirection = AttackDirection.left;
        else if (Input.GetKey(RightAttack))
            attackDirection = AttackDirection.right;
        else if (Input.GetKey(UpLeftAttack))
            attackDirection = AttackDirection.upLeft;
        else if (Input.GetKey(UpRightAttack))
            attackDirection = AttackDirection.upRight;
        else if (Input.GetKey(DownLeftAttack))
            attackDirection = AttackDirection.downLeft;
        else if (Input.GetKey(DownRightAttack))
            attackDirection = AttackDirection.downRight;

        HandleWeaponMovement();

        if (Input.GetKeyDown(KeyCode.Mouse0) && Cursor.visible)
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
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking && staminaBar.stamina>0)
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
        staminaBar.DecreaseStamina();
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
    }

    void HandleWeaponMovement()
    {
        switch (attackDirection)
        {
            case AttackDirection.up:
                transform.localPosition = new Vector3(0, orbitDistance, 0);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                break;
            case AttackDirection.down:
                transform.localPosition = new Vector3(0, -orbitDistance, 0);
                transform.localRotation = Quaternion.Euler(0, 0, 180);
                break;
            case AttackDirection.left:
                transform.localPosition = new Vector3(-orbitDistance, 0, 0);
                transform.localRotation = Quaternion.Euler(0, 0, 90);
                break;
            case AttackDirection.right:
                transform.localPosition = new Vector3(orbitDistance, 0, 0);
                transform.localRotation = Quaternion.Euler(0, 0, -90);
                break;
            case AttackDirection.upLeft:
                transform.localPosition = new Vector3(-orbitDistance / Mathf.Sqrt(2), orbitDistance / Mathf.Sqrt(2), 0);
                transform.localRotation = Quaternion.Euler(0, 0, 45);
                break;
            case AttackDirection.upRight:
                transform.localPosition = new Vector3(orbitDistance / Mathf.Sqrt(2), orbitDistance / Mathf.Sqrt(2), 0);
                transform.localRotation = Quaternion.Euler(0, 0, -45);
                break;
            case AttackDirection.downLeft:
                transform.localPosition = new Vector3(-orbitDistance / Mathf.Sqrt(2), -orbitDistance / Mathf.Sqrt(2), 0);
                transform.localRotation = Quaternion.Euler(0, 0, 135);
                break;
            case AttackDirection.downRight:
                transform.localPosition = new Vector3(orbitDistance / Mathf.Sqrt(2), -orbitDistance / Mathf.Sqrt(2), 0);
                transform.localRotation = Quaternion.Euler(0, 0, -135);
                break;
        }
    }

}
