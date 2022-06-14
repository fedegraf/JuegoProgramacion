using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject controllsCanvas;
    [SerializeField] private GameObject optionsCanvas;
    [Header("Buttons")]
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button controllsButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitGameButton;
    [SerializeField] private Button[] backButtons;
    [Header("Game Info")]
    [SerializeField] private Text gameNameText;
    [SerializeField] private Text gameVersionText;

    private GameObject _currentActiveCanvas;

    private void Awake()
    {
        SetButtons();

        optionsCanvas.SetActive(false);
        controllsCanvas.SetActive(false);
        SetCanvas(mainMenuCanvas);

        gameNameText.text = Application.productName;
        gameVersionText.text = Application.version;
    }

    private void SetButtons()
    {
        startGameButton.onClick.AddListener(StartGameButtonOnClickHandler);
        controllsButton.onClick.AddListener(ControllsButtonOnClickHandler);
        optionsButton.onClick.AddListener(OptionsButtonOnClickHandler);
        quitGameButton.onClick.AddListener(QuitGameButtonOnClickHandler);

        for (int i = 0; i < backButtons.Length; i++)
        {
            backButtons[i].onClick.AddListener(BackButtonOnClickHandler);
        }
    }

    private void SetCanvas(GameObject newCanvas)
    {
        if(_currentActiveCanvas)
            _currentActiveCanvas.SetActive(false);

        _currentActiveCanvas = newCanvas;
        _currentActiveCanvas.SetActive(true);
    }

    private void StartGameButtonOnClickHandler()
    {
        GameManager.Instance.SetState(GameManager.GameStates.Playing);
    }
    private void ControllsButtonOnClickHandler()
    {
        SetCanvas(controllsCanvas);
    }

    private void OptionsButtonOnClickHandler()
    {
        SetCanvas(optionsCanvas);
    }

    private void QuitGameButtonOnClickHandler()
    {
        GameManager.Instance.QuitGame();
    }

    private void BackButtonOnClickHandler()
    {
        SetCanvas(mainMenuCanvas);
    }
}
