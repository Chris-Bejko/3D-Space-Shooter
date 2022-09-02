using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{

    [SerializeField]
    private float _timeForDestruction;

    [SerializeField]
    private EnemyConfig _thisData;

    [SerializeField]
    private Transform[] firingPoints;

    [SerializeField]
    private int scoreToAdd;

    private Transform _player;

    private int _health;

    private BulletParent _bulletParent;

    public int ID;

    private void Awake()
    {
        _health = _thisData.Health;
        SetBulletParent(BulletParent.Enemy);
    }

    private void OnEnable()
    {
        if (GameManager.Instance)
            _player = GameManager.Instance.player.transform;

        if (!_player)
            return;

        StartCoroutine(Move());
        StartCoroutine(Fire());
    }

    void Update()
    {
        if (GameManager.Instance.gameState != GameManager.GameState.Playing)
            return;

        //Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            if (damageable.GetBulletParent() == _bulletParent)
                return;

            damageable.TakeDamage(_thisData.CollisionDamage);
        }

        Destruct();
    }

    #region IDamageable

    public void Destruct()
    {
        StartCoroutine(DestroyShip());
    }

    private IEnumerator DestroyShip()
    {
        GameManager.Instance.UiManager.GameUI.AddScore(scoreToAdd);
        ///To-do: Play animations
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
        if(_health <= 0) 
            Destruct(); 
    }   

    public BulletParent GetBulletParent()
    {
        return _bulletParent;
    }

    public void SetBulletParent(BulletParent parent)
    {
        _bulletParent = parent;
    }

    #endregion

    #region Functions

    private IEnumerator Move()
    {
        while (Vector3.Distance(_player.position, transform.position) >= 0.1f)
        {
            var step = _thisData.MoveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _player.position, step);
            transform.LookAt(_player.position);
            yield return null;
        }
        Destruct();
    }

    private IEnumerator Fire()
    {
        yield return new WaitUntil(() => Vector3.Distance(_player.position, transform.position) <= _thisData.FiringDistance);

        while (Vector3.Distance(_player.position, transform.position) >= 0.01f)
        {
            GameObject temp = null;
            for (int i = 0; i < firingPoints.Length; i++)
            {
                temp = GameManager.Instance.BulletsPool.GetPooledObject();
                temp.transform.position = firingPoints[i].position;
                temp.GetComponent<Bullet>().SetBulletParent(_bulletParent);
                temp.SetActive(true);
            }
            yield return new WaitForSeconds(_thisData.FiringCooldown);
        }

    }

    #endregion
}
