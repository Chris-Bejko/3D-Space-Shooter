using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    [SerializeField]
    TMP_Text highscoreText;

    private void OnEnable()
    {
        UpdateHighscoreVisuals(GameManager.Instance.Highscore);
    }

    public void ContinueButton()
    {
        GameManager.Instance.HandleStateChange(GameManager.GameState.Playing);
    }

    public void MainMenuButton()
    {
        GameManager.Instance.HandleStateChange(GameManager.GameState.Menu);
    }


    public void UpdateHighscoreVisuals(int newScore)
    {
        highscoreText.text = "Highscore: " + newScore;
    }
}
