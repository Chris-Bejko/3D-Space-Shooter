using UnityEngine;

[CreateAssetMenu(fileName = "Waves", menuName = "Scriptables/WaveData", order = 0)]
public class Waves : ScriptableObject
{
    public GameObject[] EnemiesPrefabs;

    public int TotalEnemies;

    public Transform[] EnemySpawnPoint;

    public float TimeBetweenSpawns;
}
