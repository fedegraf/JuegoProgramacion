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

    public enum GameStates
    {
        Playing,
        Paused,
        Dead,
        MainMenu,
        Close
    }

    public GameStates CurrentState { get; private set; }


    private void Awake()
    {
        MakeSingleton();
    }

    void Start()
    {
        SetState(GameStates.Playing);
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

    private void GamePlaying()
    {
        Time.timeScale = 1; 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void GamePaused()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //Spawn Puase Menu
    }

    public void SetState(GameStates newState)
    {
        switch (newState)
        {
            case GameStates.Playing:
                GamePlaying();
                OnGamePlaying?.Invoke();
                break;
            case GameStates.Paused:
                GamePaused();
                OnGamePaused?.Invoke();
                break;
            case GameStates.Dead:
                GamePaused();
                OnDead?.Invoke();
                break;
            case GameStates.MainMenu:
                GamePaused();
                OnMainMenu?.Invoke();
                break;
            case GameStates.Close:
                QuitGame();
                break;
        }

        CurrentState = newState;
    }

    private void QuitGame()
    {
        Application.Quit();
    }

}
