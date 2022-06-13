using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject hudCanvas;
    [SerializeField] private GameObject pauseMenuCanvas;
    [SerializeField] private GameObject deadMenuCanvas;

    private void Start()
    {
        GameManager.Instance.OnGamePaused += OnGamePausedHandler;
        GameManager.Instance.OnGamePlaying += OnGamePlayingHandler;
        GameManager.Instance.OnDead += OnDeadHandler;
    }

    private void OnGamePausedHandler()
    {
        hudCanvas.SetActive(false);
        deadMenuCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(true);
    }

    private void OnGamePlayingHandler()
    {
        pauseMenuCanvas.SetActive(false);
        deadMenuCanvas.SetActive(false);
        hudCanvas.SetActive(true);

    }

    private void OnDeadHandler()
    {
        hudCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(false);
        deadMenuCanvas.SetActive(true);
    }
}
