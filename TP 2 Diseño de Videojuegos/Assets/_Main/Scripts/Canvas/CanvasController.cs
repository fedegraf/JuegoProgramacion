using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject hudCanvas;
    [SerializeField] private GameObject pauseMenuCanvas;
    [SerializeField] private GameObject deadMenuCanvas;
    [SerializeField] private GameObject winMenuCanvas;

    private GameObject _currentActiveCanvas;

    private void Awake()
    {
        pauseMenuCanvas.SetActive(false);
        deadMenuCanvas.SetActive(false);
        SetCanvas(hudCanvas);
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += OnGamePausedHandler;
        GameManager.Instance.OnGamePlaying += OnGamePlayingHandler;
        GameManager.Instance.OnDead += OnDeadHandler;
        GameManager.Instance.OnWin += OnWinHandler;
    }

    private void SetCanvas(GameObject newCanvas)
    {
        if (!newCanvas)
        {
            Debug.Log("New Canvas is Null");
            return;
        }

        if (_currentActiveCanvas)
            _currentActiveCanvas.SetActive(false);

        _currentActiveCanvas = newCanvas;
        _currentActiveCanvas.SetActive(true);
    }

    private void OnGamePausedHandler()
    {
        SetCanvas(pauseMenuCanvas);
    }

    private void OnGamePlayingHandler()
    {
        SetCanvas(hudCanvas);
    }

    private void OnDeadHandler()
    {
        SetCanvas(deadMenuCanvas);
    }

    private void OnWinHandler()
    {
        //Debug.Log("Won");
        SetCanvas(winMenuCanvas);
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGamePaused -= OnGamePausedHandler;
        GameManager.Instance.OnGamePlaying -= OnGamePlayingHandler;
        GameManager.Instance.OnDead -= OnDeadHandler;
    }
}
