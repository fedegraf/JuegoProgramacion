using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    private Character _character;

    [Range(0, 200)]
    [SerializeField] private float mouseSensivity;

    [Header("Inputs Names")]
    [SerializeField] private string mouseXInput;
    [SerializeField] private string mouseYInput;
    [SerializeField] private string horizontalInput;
    [SerializeField] private string verticalInput;
    [SerializeField] private string runInput;
    [SerializeField] private string shootInput;
    [SerializeField] private string reloadInput;


    private void Awake()
    {
        _character = GetComponent<Character>();
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
        var vAxis = Input.GetAxis(verticalInput);
        var hAxis = Input.GetAxis(horizontalInput);

        Vector2 inputDirection = new Vector2(vAxis, hAxis);

        bool runInput = Input.GetButton(this.runInput);

        _character.DoWalking(inputDirection, runInput);
    }

    private void Rotation()
    {
        float x = Input.GetAxis(mouseXInput) * mouseSensivity * Time.deltaTime;
        float Y = Input.GetAxis(mouseYInput) * mouseSensivity * Time.deltaTime;

        Vector2 mouseInput = new Vector2(x, Y);

        _character.DoRotation(mouseInput);
    }

    private void Weapon()
    {
        if (Input.GetButton(shootInput)) _character.DoShoot();

        else if (Input.GetButtonDown(reloadInput)) _character.DoReload();
    }
}
