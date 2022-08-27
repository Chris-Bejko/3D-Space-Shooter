using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{

    [SerializeField]
    private float _timeForDestruction;

    [SerializeField]
    EnemyConfig _thisData;


    private Transform _player;

    private bool _alive;

    private Transform[] firingPoints;

    private void Awake()
    {
        _player = GameManager.Instance.player.transform;
        _alive = true;
        firingPoints = _thisData.ShootPoints;
    }

    private void OnEnable()
    {
        StartCoroutine(Move());
    }

    void Update()
    {
        if (GameManager.Instance.gameState != GameManager.GameState.Playing)
            return;

        //Move();
    }

    public void Destruct()
    {
        StartCoroutine(DestroyShip());
    }

    private IEnumerator DestroyShip()
    {
        ///To-do: Play animations
        yield return new WaitForSeconds(_timeForDestruction);
        _alive = false;
        gameObject.SetActive(false);
    }

    public int GetHealth()
    {
        return _thisData.Health;
    }

    public void TakeDamage(int damage)
    {
        _thisData.Health -= damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(_thisData.CollisionDamage);
        }

        Destruct();
    }

    #region Functions

    private IEnumerator Move()
    {
        while (Vector3.Distance(_player.position, transform.position) >= 0.1f)
        {
            var step = _thisData.MoveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _player.position, step);
            transform.LookAt(_player.position);
            Fire();
            yield return null;
        }
        Destruct();
    }

    private void Fire()
    {
        if (Vector3.Distance(_player.position, transform.position) >= _thisData.FiringDistance)
            return;

        GameObject temp = null;
        for(int i = 0; i < _thisData.ShootPoints.Length; i++)
        {
            temp = GameManager.Instance.bulletsPool.GetPooledObject();
            temp.transform.position = firingPoints[i].position;
            temp.SetActive(true);
        }
    }
    #endregion
}
