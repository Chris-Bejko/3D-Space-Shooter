using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    [SerializeField]
    TMP_Text lostText, titleText, highscoreText;
    public void Awake()
    {
        GameManager.OnGameStateChanged += HandleStateChange;
        UpdateHighscoreVisuals(PlayerPrefs.GetInt("Highscore"));
    }

    public void PlayButton()
    {
        GameManager.Instance.HandleStateChange(GameManager.GameState.Playing);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void HandleStateChange(GameManager.GameState newState)
    {
        lostText.gameObject.SetActive(newState == GameManager.GameState.Lost);
        titleText.gameObject.SetActive(newState == GameManager.GameState.Menu);
        highscoreText.gameObject.SetActive(newState == GameManager.GameState.Lost || newState == GameManager.GameState.Menu);

    }

    public void UpdateHighscoreVisuals(int newScore)
    {
       highscoreText.text = "Highscore: " + newScore;
    }
}
