using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private float _bulletForce;
    [SerializeField]
    private int _bulletDamage;

    private void OnEnable()
    {
        Shoot();
    }

    private void Shoot()
    {
        Debug.LogError("Bullet Shooting");
        _rigidbody.AddForce(transform.forward * _bulletForce);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _rigidbody.AddExplosionForce(_bulletForce, transform.position, 2);

        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(_bulletDamage);

            if (damageable.GetHealth() <= 0)
                damageable.Destruct();
        }

        gameObject.SetActive(false);
    }
}
