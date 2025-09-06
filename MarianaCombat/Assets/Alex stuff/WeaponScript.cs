using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class WeaponScript : MonoBehaviour
{
    float weaponScaler, weaponEx, minScale;
    public int damage;
    int orbitDistance;
    public GameObject player; // Reference to the player

    private void Start()
    {
        weaponScaler = 0.92f;
        weaponEx = 18.21f;
        minScale = 0.7f;
        orbitDistance = 1;
    }

    void Update()
    {
        HandleWeaponMovement();

        if (Input.GetKeyDown(KeyCode.Mouse0) && Cursor.visible == true)
        {
            Cursor.visible = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Cursor.visible == false)
        {
            Cursor.visible = true;
        }
    }

    void HandleWeaponMovement()
    {
        if (player == null) return; // Safety check

        // Get mouse position in world space
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Camera.main.WorldToScreenPoint(player.transform.position).z;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        // Calculate direction from player to mouse
        Vector3 playerPosition = player.transform.position;
        Vector3 direction = mouseWorldPosition - playerPosition;

        // Set weapon position at a fixed distance from player, along mouse direction
        transform.position = playerPosition + direction * orbitDistance;

        // Scale weapon based on distance from player to mouse, using scaler and exponent
        float mouseDistance = Vector3.Distance(mouseWorldPosition, playerPosition);
        float scale = mouseDistance * Mathf.Pow(weaponScaler, weaponEx);
        transform.localScale = Vector3.one * scale;

        if (transform.localScale.x < minScale)
            transform.localScale = new Vector3 (minScale, minScale, minScale);

        // Rotate weapon to face the mouse (2D)
        Vector3 lookDir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90);
    }
}
