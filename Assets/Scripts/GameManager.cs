using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public ObjectPool bulletsPool;

    public bool IsPaused { get; set; }


    private void Awake()
    {
        Instance = this;
    }

}
