using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float ShootingCooldown;

    public float TimeBetweenSameShot;

    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private float _bulletForce;

    [SerializeField]
    private int _bulletDamage;

    [SerializeField]
    private float _timeToDisableAfterNoCollision;

    private void OnEnable()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        _rigidbody.velocity = Vector3.forward * _bulletForce;
        yield return new WaitForSeconds(_timeToDisableAfterNoCollision);
        if (gameObject.activeInHierarchy)
            gameObject.SetActive(false);
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
