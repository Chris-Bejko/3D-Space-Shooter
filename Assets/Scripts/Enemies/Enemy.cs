using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{

    public int _health;
    [SerializeField]
    private float _timeForDestruction;

    public void Destruct()
    {
        StartCoroutine(DestroyShip());
    }

    private IEnumerator DestroyShip()
    {
        yield return new WaitForSeconds(_timeForDestruction);
        gameObject.SetActive(false);
    }
    public int GetHealth()
    {
        return _health;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
    }

}
