using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public GameObject ObjectToPool;
    public int AmountToPool;


    private void Awake()
    {
        SpawnPool();
    }

    public void SpawnPool()
    {
        pooledObjects = new List<GameObject>();
        GameObject temp = null;
        for(int i = 0; i < AmountToPool; i++)
        {
            temp = Instantiate(ObjectToPool, transform);
            temp.SetActive(false);
            pooledObjects.Add(temp);
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < AmountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    public ObjectPool(GameObject ObjectToPool, int AmountToPool)
    {
        this.ObjectToPool = ObjectToPool;
        this.AmountToPool = AmountToPool;
    }

}
