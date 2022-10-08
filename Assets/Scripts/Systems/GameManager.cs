using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public ObjectPool BulletsPool;

    public ObjectPool[] EnemiesPool;

    public WaveManager WaveManager;

    public UIManager UiManager;

    public AudioManager AudioManager;

    public GameState gameState;

    public GameState previousGameState;

    public Player player;

    public Transform[] SpawnPoints;

    public int Highscore;

    public static event Action<GameState> OnGameStateChanged;



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        HandleStateChange(GameState.Menu);
    }

    #region State Functions
    public void HandleStateChange(GameState newState)
    {
        previousGameState = gameState;

        gameState = newState;

        switch (gameState)
        {
            case GameState.Playing:
                HandlePlayingState();
                break;
            case GameState.Paused:
                HandlePausedState();
                break;
            case GameState.Menu:
                HandleMenuState();
                break;
            case GameState.Lost:
                HandleLostState();
                break;
        }

        OnGameStateChanged?.Invoke(newState);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && (gameState != GameState.Menu || gameState != GameState.Lost))
            HandleStateChange(gameState == GameState.Paused ? GameState.Playing : GameState.Paused);

        Time.timeScale = gameState == GameState.Paused ? 0 : 1;
    }
    private void HandleLostState()
    {
        SetHighScore(UiManager.GameUI.GetScore());
        UiManager.HandleScreenOpen(UIScreenId.LostMenu);
    }

    private void HandleMenuState()
    {
        UiManager.HandleScreenOpen(UIScreenId.MainMenu);
    }

    private void HandlePausedState()
    {
        UiManager.HandleScreenOpen(UIScreenId.PauseMenu);
    }

    private void HandlePlayingState()
    {
        
        UiManager.HandleScreenOpen(UIScreenId.InGameMenu);
    }

    #endregion
    public void SetHighScore(int score)
    {
        if(score > Highscore)
        {
            UiManager.MainMenuUI.UpdateHighscoreVisuals(score);
            Highscore = score;
            PlayerPrefs.SetInt("Highscore", score);
        }
    }

    public enum GameState
    {
        Playing,
        ScoreUpdated,
        Paused,
        Menu,
        Lost,
        Decide
    }
}