using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour, IDamageable
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

    private int bulletHealth = 10;

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

    private void OnTriggerEnter(Collider collision)
    {
        _rigidbody.AddExplosionForce(_bulletForce, transform.position, 2);

        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(_bulletDamage);

            if (damageable.GetHealth() <= 0)
                damageable.Destruct();
        }

        TakeDamage(bulletHealth);
    }

    public void TakeDamage(int damage)
    {
        bulletHealth -= damage;
        if(bulletHealth <= 0) { Destruct(); }
    }

    public void Destruct()
    {
        ///Play destroy animation
        gameObject.SetActive(false);
    }

    public int GetHealth()
    {
        return bulletHealth;
    }
}
