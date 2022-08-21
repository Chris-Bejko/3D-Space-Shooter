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
    private Vector3 _bounds;


    private float _horizontal, _vertical;

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
        _rigidbody.MovePosition(_rigidbody.position + new Vector3(_horizontal, 0, _vertical) * Time.fixedDeltaTime * _speed);
        _rigidbody.position = new Vector3(Mathf.Clamp(_rigidbody.position.x, -_bounds.x, _bounds.x), 
            Mathf.Clamp(_rigidbody.position.y, -_bounds.y, _bounds.y),
            Mathf.Clamp(_rigidbody.position.z, -_bounds.z, _bounds.z));
    }

    private void GetInput()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
    }
    #endregion

    #region Damage
    public void TakeDamage()
    {
    }
    #endregion
}
