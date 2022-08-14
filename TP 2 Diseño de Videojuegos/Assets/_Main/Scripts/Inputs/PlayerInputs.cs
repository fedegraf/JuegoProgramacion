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
    [SerializeField] private string skillInput;
    [SerializeField] private string grenadeInput = "Grenade";
    [SerializeField] private string reloadInput;
    [SerializeField] private string useItem;
    [SerializeField] private string cycleWeapons;


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
        Skill();
        Grenade();
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

        _character.DoWalking(inputDirection, false);
    }

    private void Rotation()
    {
        var positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        Vector3 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        var direction = mouseOnScreen - positionOnScreen;

        _character.DoRotation(direction);
    }

    private void Weapon()
    {
        if (Input.GetButton(shootInput)) _character.DoShoot();
        else if (Input.GetButtonDown(reloadInput)) _character.DoReload();
        else if (Input.GetButtonDown(cycleWeapons)) _character.DoCycleWeapons();
    }

    private void Item()
    {
        if (Input.GetButtonDown(useItem)) _character.DoItemUse();
    }

    private void Skill()
    { 
        if(Input.GetButtonDown(skillInput)) _character.DoSkill();
    }

    private void Grenade()
    {
        if(Input.GetButtonDown(grenadeInput)) _character.TrhowGrenade();
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
