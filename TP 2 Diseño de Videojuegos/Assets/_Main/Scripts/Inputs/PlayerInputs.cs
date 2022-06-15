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
    [SerializeField] private string pickUpInput;
    [SerializeField] private string itemInGroundUse;
    [SerializeField] private string showInventory;


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
        if (_character &&
            GameManager.Instance.CurrentState == GameManager.GameStates.Playing)
        {
            CharacterInputs();
        }

        if (GameManager.Instance.CurrentState != GameManager.GameStates.Dead)
            UIInputs();
    }

    private void CharacterInputs()
    {
        Walking();
        Rotation();
        Weapon();
        Item();
    }

    private void UIInputs()
    {
        TogglePauseGame();
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

    private void Item()
    {
        if (Input.GetButtonDown(pickUpInput)) _character.DoPickUp();
        else if (Input.GetButtonDown(itemInGroundUse)) _character.DoItemInGroundUse();
        else if (Input.GetButtonDown(showInventory)) _character.DoShowItems();
    }

    private void TogglePauseGame()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameManager.Instance.CurrentState == GameManager.GameStates.Playing)
                GameManager.Instance.SetState(GameManager.GameStates.Paused);
            else if (GameManager.Instance.CurrentState == GameManager.GameStates.Paused)
                GameManager.Instance.SetState(GameManager.GameStates.Playing);
        }
    }
}
