using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public ObjectPool bulletsPool;

    public WaveManager waveManager;

    public GameState gameState;

    public Player player;

    public Transform[] spawnPoints;

    public int Score { get; set; }

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
        HandleStateChange(GameState.Playing);
    }

    #region State Functions
    private void HandleStateChange(GameState newState)
    {
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
            case GameState.Decide:
                HandleDecideState();
                break;
        }

        OnGameStateChanged?.Invoke(newState);

    }
    private void HandleDecideState()
    {

    }

    private void HandleLostState()
    {
    }

    private void HandleMenuState()
    {
    }

    private void HandlePausedState()
    {
    }

    private void HandlePlayingState()
    {
    }

    #endregion


    public enum GameState
    {
        Playing,
        Paused,
        Menu,
        Lost,
        Decide
    }
}