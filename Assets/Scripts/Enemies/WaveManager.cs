using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    public Waves[] waves;

    public float spawnTimer = 2f;

    private void Awake()
    {
        GameManager.OnGameStateChanged += OnGameStateChange;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= OnGameStateChange;
    }
    private void OnGameStateChange(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Playing && GameManager.Instance.previousGameState != GameManager.GameState.Paused)
            StartCoroutine(SpawnWaves());
        else
            StopCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (GameManager.Instance.gameState == GameManager.GameState.Playing)
        {
            int index = Random.Range(0, waves.Length);
            var currentWave = waves[index];
            yield return StartCoroutine(SpawnEnemiesInWave(currentWave, index));
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    private IEnumerator SpawnEnemiesInWave(Waves currentWave, int index)
    {
        for (int i = 0; i < currentWave.TotalEnemies; i++)
        {
            var temp = GameManager.Instance.EnemiesPool[currentWave.EnemiesPrefabs[i].GetComponent<Enemy>().ID].GetPooledObject();
            temp.transform.position = GameManager.Instance.SpawnPoints[index].GetChild(i).position;
            temp.SetActive(true);
            yield return new WaitForSeconds(currentWave.TimeBetweenSpawns);
        }
    }


}
