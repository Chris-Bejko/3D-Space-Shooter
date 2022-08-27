using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public int PlayerHealth;


    [SerializeField]
    private float _speed;

    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private Vector3 _minBounds;

    [SerializeField]
    private Vector3 _maxBounds;


    private float _horizontal, _vertical;

    private BulletParent _bulletParent;

    [SerializeField]
    private float _secondsToDestruct;

    [SerializeField]
    private int _totalBulletsSpawnPoints;

    [SerializeField]
    private Transform[] _spawnPoints;


    #region Unity Functions

    private void Awake()
    {
        SetBulletParent(BulletParent.Player);
    }
    private void Update()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.Paused)
            return;

        GetInput();
        Shoot();
    }

    private void FixedUpdate()
    {
        Move();
    }

    #endregion

    #region Movement
    private void Move()
    {
        _rigidbody.velocity = (new Vector3(_horizontal, _vertical) * Time.fixedDeltaTime * _speed);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, _minBounds.x, _maxBounds.x),
             Mathf.Clamp(transform.position.y, _minBounds.y, _maxBounds.y),
           transform.position.z);
    }

    private void GetInput()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
    }
    #endregion

    #region Damage
    public void TakeDamage(int damage)
    {
        PlayerHealth -= damage;
    }

    public int GetHealth()
    {
        return PlayerHealth;
    }
    public void Destruct()
    {
        StartCoroutine(StartDestruction());
    }

    public IEnumerator StartDestruction()
    {
        ///To do - play animations
        yield return new WaitForSeconds(_secondsToDestruct);
        gameObject.SetActive(false);
        GameManager.Instance.HandleStateChange(GameManager.GameState.Lost);
    }
    #endregion

    #region Shooting

    public void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(FireCoroutine());
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            StopAllCoroutines();
        }
    }

    public IEnumerator FireCoroutine()
    {
        while (true)
        {
            GameObject temp = null;
            for (int i = 0; i < _totalBulletsSpawnPoints; i++)
            {
                temp = GameManager.Instance.bulletsPool.GetPooledObject();
                temp.transform.position = _spawnPoints[i].position;
                temp.GetComponent<Bullet>().SetBulletParent(_bulletParent);
                temp.SetActive(true);
                yield return new WaitForSeconds(temp.GetComponent<Bullet>().TimeBetweenSameShot);
            }
            yield return new WaitForSeconds(temp.GetComponent<Bullet>().ShootingCooldown);
        }
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
}
