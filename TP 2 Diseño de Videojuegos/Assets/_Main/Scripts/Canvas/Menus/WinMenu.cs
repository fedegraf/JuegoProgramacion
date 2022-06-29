using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        mainMenuButton.onClick.AddListener(MainMenuButtonOnClickHandler);
        quitButton.onClick.AddListener(QuitButtonOnClickHandler);
    }

    private void MainMenuButtonOnClickHandler()
    {
        GameManager.Instance.SetState(GameManager.GameStates.InitScreen);
    }

    private void QuitButtonOnClickHandler()
    {
        GameManager.Instance.QuitGame();
    }
}
