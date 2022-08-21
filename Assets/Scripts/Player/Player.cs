using System.Collections;
using System.Collections.Generic;
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



    [SerializeField]
    private float _secondsToDestruct;

    [SerializeField]
    private int _totalBulletsSpawnPoints;

    [SerializeField]
    private Transform[] _spawnPoints;


    #region Unity Functions
    private void Update()
    {
        if (GameManager.Instance.IsPaused)
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
        _rigidbody.velocity = (new Vector3(_horizontal, 0, _vertical) * Time.fixedDeltaTime * _speed);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, _minBounds.x, _maxBounds.x),
            transform.position.y,
            Mathf.Clamp(transform.position.z, _minBounds.z, _maxBounds.z));
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
        ///One left , and one right (2 bullets)
        while (true)
        {
            GameObject temp = null;
            for (int i = 0; i < _totalBulletsSpawnPoints; i++)
            {
                temp = GameManager.Instance.bulletsPool.GetPooledObject();
                temp.transform.position = _spawnPoints[i].position;
                temp.SetActive(true);
                yield return new WaitForSeconds(temp.GetComponent<Bullet>().TimeBetweenSameShot);
            }
            yield return new WaitForSeconds(temp.GetComponent<Bullet>().ShootingCooldown);
        }
    }

    #endregion
}
