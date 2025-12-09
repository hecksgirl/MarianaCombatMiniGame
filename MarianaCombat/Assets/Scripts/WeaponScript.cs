using System.Collections;
using UnityEngine;
using System;

public class WeaponScript : MonoBehaviour
{
    public float parryDuration;
    public int damage;
    public float orbitDistance;
    public bool isAttacking;
    public GameObject player;
    Animator animator;
    public KeyCode pauseKey, AltPauseKey, UpAttack, DownAttack, LeftAttack, RightAttack, parryKey;
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

        isAttacking = false;

        staminaBar = FindFirstObjectByType<StaminaBar>();
    }

    void Update()
    {
        attackDirection = AttackDirection.neutral;

        
        if (Input.GetKey(UpAttack)&&Input.GetKey(LeftAttack))
        {
            attackDirection = AttackDirection.upLeft;
            Debug.Log("UpLeft detected");
        }
        else if (Input.GetKey(UpAttack)&&Input.GetKey(RightAttack))
        {
            attackDirection = AttackDirection.upRight;
            Debug.Log("UpRight detected");
        }
        else if (Input.GetKey(DownAttack)&&Input.GetKey(LeftAttack))
        {
            attackDirection = AttackDirection.downLeft;
            Debug.Log("DownLeft detected");
        }
        else if (Input.GetKey(DownAttack)&&Input.GetKey(RightAttack))
        {
            attackDirection = AttackDirection.downRight;
            Debug.Log("DownRight detected");
        }
        else if (Input.GetKey(UpAttack))
            attackDirection = AttackDirection.up;
        else if (Input.GetKey(DownAttack))
            attackDirection = AttackDirection.down;
        else if (Input.GetKey(LeftAttack))
            attackDirection = AttackDirection.left;
        else if (Input.GetKey(RightAttack))
            attackDirection = AttackDirection.right;

        HandleWeaponMovement();

        if (Input.GetKeyDown(KeyCode.Mouse0) && Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if ((Input.GetKeyDown(pauseKey) || Input.GetKeyDown(AltPauseKey)) && !Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(parryKey))
        {
            Parry();
        }
        if (attackDirection!= AttackDirection.neutral && staminaBar.stamina>0)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;

        AudioManager.Instance.PlaySwordSwing();

        animator.SetBool("Attacking", true);
        staminaBar.DecreaseStamina();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetBool("Attacking", false);
        isAttacking = false;
    }

    void Parry()
    {
        Debug.Log("Parry attempted");
        GreatWhite gw= FindFirstObjectByType<GreatWhite>();
        animator.SetTrigger("Parry");
        if (gw != null && gw.attackSuccessful)
        { 
            gw.attackSuccessful = false;

            Debug.Log("Parry successful");
        }
        else
        {
            Debug.Log("Parry failed");
        }
    }

    void HandleWeaponMovement()
    {
        switch (attackDirection)
        {
            case AttackDirection.neutral:
                transform.localPosition = new Vector3(0, 0, 0);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                break;
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
