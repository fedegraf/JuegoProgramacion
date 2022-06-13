using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        resumeButton.onClick.AddListener(ResumeButtonOnClickHandler);
        mainMenuButton.onClick.AddListener(MainMenuButtonOnClickHandler);
        quitButton.onClick.AddListener(QuitButtonOnClickHandler);
    }

    private void ResumeButtonOnClickHandler()
    {
        GameManager.Instance.SetState(GameManager.GameStates.Playing);
    }

    private void MainMenuButtonOnClickHandler()
    {
        GameManager.Instance.SetState(GameManager.GameStates.MainMenu);
    }

    private void QuitButtonOnClickHandler()
    {
        GameManager.Instance.SetState(GameManager.GameStates.Close);
    }
}
