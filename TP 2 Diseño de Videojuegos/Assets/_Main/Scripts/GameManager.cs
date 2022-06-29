using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public UnityAction OnGamePlaying;
    public UnityAction OnGamePaused;
    public UnityAction OnDead;
    public UnityAction OnMainMenu;
    public UnityAction OnWin;

    private SoundManager _sound;


    public enum GameStates
    {
        Playing,
        Paused,
        Dead,
        InitScreen,
        Win
    }

    public GameStates CurrentState { get; private set; }
    [SerializeField] private GameStates initialState;


    private void Awake()
    {        
        MakeSingleton();
        Cursor.lockState = CursorLockMode.Confined;
        _sound = GetComponent<SoundManager>();
    }

    void Start()
    {
        if (initialState == GameStates.Paused || initialState == GameStates.Dead)
            initialState = GameStates.Playing;

        SetState(initialState);
    }

    private void MakeSingleton()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void GamePaused()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    public void SetState(GameStates newState)
    {
        switch (newState)
        {
            case GameStates.Playing:
                StatePlaying();
                break;
            case GameStates.Paused:
                StatePaused();
                break;
            case GameStates.Dead:
                StateDead();
                break;
            case GameStates.InitScreen:
                StateInitScreen();
                break;
            case GameStates.Win:
                StateGameWon();
                break;
        }

        CurrentState = newState;
    }

    private void StatePlaying()
    {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Level 1"))
        {
            SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
        }

        _sound.PlaySound("BGM");
        Cursor.visible = false;
        Time.timeScale = 1;        
        OnGamePlaying?.Invoke();
    }

    private void StatePaused()
    {
        GamePaused();
        OnGamePaused?.Invoke();
    }

    private void StateDead()
    {
        GamePaused();
        OnDead?.Invoke();
    }

    private void StateInitScreen()
    {
        _sound.PlaySound("BGM");
        GamePaused();
        OnMainMenu?.Invoke();
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("MainMenu"))
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }

    private void StateGameWon()
    {
        GamePaused();
        OnWin?.Invoke();
    }

    public void ReloadScene()
    {
        SetState(GameStates.Playing);
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
