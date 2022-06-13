using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadMenu : MonoBehaviour
{
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        playAgainButton.onClick.AddListener(PlayAgainButtonOnClickHandler);
        mainMenuButton.onClick.AddListener(MainMenuButtonOnClickHandler);
        quitButton.onClick.AddListener(QuitButtonOnClickHandler);

        playAgainButton.gameObject.SetActive(false);
    }

    private void PlayAgainButtonOnClickHandler()
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
