using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    public List<GameObject> TotalWavesSpawned = new List<GameObject>();

    public List<GameObject> StaticWavesSpawned = new List<GameObject>();
    public float offset;

    public int CurrentWaveIndex;

    [SerializeField] 
    private int _maxWavesToSpawn; 

    [SerializeField]
    private Transform _playerTransform;

    private int wavePoolCount;

    private void Start()
    {
        for(int i = 0; i < StaticWavesSpawned.Count; i++)
        {
            TotalWavesSpawned.Add(StaticWavesSpawned[i]);
        }
        CurrentWaveIndex = TotalWavesSpawned.Count - 1;
        wavePoolCount = GameManager.Instance.wavesPools.Length;
    }


    private void Update()
    {
        SpawnWaves();
    }

    private void SpawnWaves()
    {
        if (_playerTransform.position.x <= (TotalWavesSpawned[CurrentWaveIndex].transform.position.x) - offset && TotalWavesSpawned.Count <= _maxWavesToSpawn)
        {
            GameObject nextTile = GameManager.Instance.wavesPools[Random.Range(0, wavePoolCount)].GetPooledObject();

            Debug.Log(nextTile.name);

            TotalWavesSpawned.Add(nextTile);  

            nextTile.transform.position = TotalWavesSpawned[CurrentWaveIndex].transform.position - new Vector3(10, 0);

            nextTile.SetActive(true);

            CurrentWaveIndex++;
        }
        else if (TotalWavesSpawned.Count > _maxWavesToSpawn)
        {
            var toBeRemoved = TotalWavesSpawned[CurrentWaveIndex - _maxWavesToSpawn];

            toBeRemoved.SetActive(false);

            TotalWavesSpawned.Remove(toBeRemoved);

            CurrentWaveIndex = TotalWavesSpawned.Count - 1;
        }
    }
}
