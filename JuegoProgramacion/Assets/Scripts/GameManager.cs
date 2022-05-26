using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static Transform respawnPoint;
    [SerializeField] private GameObject player;
    private static bool isPlaying;
    public static VoidDelegate StartGameEvent;
    public static VoidDelegate GameOverEvent;
    public static VoidDelegate WinGameEvent;

    public static bool IsPlaying { get => isPlaying; }

    // Start is called before the first frame update
    void Start()
    {
   /*
        Time.timeScale = 0;
        isPlaying = false;
        Cursor.visible = true;
        player.GetComponent<HealthScript>().DeathEvent += GameOver;
    */
   StartPlay();
    }

    void GameOver()
    {
        isPlaying = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameOverEvent?.Invoke();
    }

    public static void WinGame()
    {
        isPlaying = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        WinGameEvent?.Invoke();
    }

    public void StartPlay()
    {
        isPlaying = true;
        Time.timeScale = 1; 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartGameEvent?.Invoke();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
