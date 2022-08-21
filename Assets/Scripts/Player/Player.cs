using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private Vector3 _minBounds;
    [SerializeField]
    private Vector3 _maxBounds;


    private float _horizontal, _vertical;



    public int PlayerHealth;

    #region Unity Functions
    private void Update()
    {
        GetInput();
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

    public void Destruct(float seconds)
    {
        StartCoroutine(StartDestruction(seconds));
    }

    public IEnumerator StartDestruction(float seconds)
    {
        ///To do - play animations
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
    #endregion
}
