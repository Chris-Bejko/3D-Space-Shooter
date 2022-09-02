using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;

    [SerializeField]
    private Slider healthSlider;

    private int _score;

    public int GetScore()
    {
        return _score;
    }

    public void AddScore(int score)
    {
        _score += score;
        _scoreText.text = _score.ToString();
    }

    private void Awake()
    {
        AddScore(0);
        UpdatePlayerHealth(100);
    }

    public void UpdatePlayerHealth(int health)
    {
        healthSlider.value = health;
    }

}
