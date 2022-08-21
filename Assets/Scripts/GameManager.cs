using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public ObjectPool bulletsPool;

    public ObjectPool[] wavesPools;

    public bool IsPaused { get; set; }

    public int Score { get; set; }

    private void Start()
    {
        //EnableWaveChildren();

        Debug.Log(wavesPools == null);
    }

    private void Awake()
    {
        Instance = this;
    }

    private void EnableWaveChildren()
    {
        for (int i = 0; i < wavesPools.Length; i++)
        {
            for (int j = 0; j < wavesPools[i].transform.childCount; j++)
            {
                Debug.Log(wavesPools[i].transform.GetChild(j).gameObject.name);
                wavesPools[i].transform.GetChild(j).gameObject.SetActive(true);
            }
        }
    }
}
