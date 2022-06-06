using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    private Character _character;
    private BulletShooter _shooter;
    [Range(0, 100)]
    [SerializeField] private float mouseSensivity;

    private void Awake()
    {
        _character = GetComponent<Character>();
        _shooter = GetComponent<BulletShooter>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (_character) CharacterInputs();
    }

    private void CharacterInputs()
    {
        Walking();
        Rotation();
        Weapon();
    }

    private void Walking()
    {
        var vAxis = Input.GetAxis("Vertical");
        var hAxis = Input.GetAxis("Horizontal");

        Vector2 inputDirection = new Vector2(vAxis, hAxis);

        _character.DoWalking(inputDirection);
    }

    private void Rotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;

        Vector2 mouseInput = new Vector2(mouseX, mouseY);

        _character.DoRotation(mouseInput);
    }

    private void Weapon()
    {
        if (Input.GetButton("Shoot"))
        {
            _shooter.DoShoot();
        }

        else if (Input.GetButtonDown("Reload"))
        {
            _shooter.DoReloadAmmo();
        }
    }
}
