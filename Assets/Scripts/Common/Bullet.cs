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

    [SerializeField]
    private AudioClip _bulletShootClip;

    private int _bulletHealth = 10;

    private BulletParent _bulletParent;

    private void OnEnable()
    {
        if (GameManager.Instance.gameState != GameManager.GameState.Playing)
            return;

        Debug.LogError(GameManager.Instance.gameState);

        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        if (_bulletParent == BulletParent.Enemy)
            _rigidbody.velocity = -Vector3.forward * _bulletForce;
        else
            _rigidbody.velocity = Vector3.forward * _bulletForce;

        GameManager.Instance.AudioManager.PlayInGameSound(_bulletShootClip);

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
            if (damageable.GetBulletParent() == _bulletParent)
                return;

            damageable.TakeDamage(_bulletDamage);
        }

        TakeDamage(_bulletHealth);
    }

    public void TakeDamage(int damage)
    {
        _bulletHealth -= damage;
        if(_bulletHealth <= 0) { Destruct(); }
    }

    public void Destruct()
    {
        ///Play destroy animation
        gameObject.SetActive(false);
    }

    public int GetHealth()
    {
        return _bulletHealth;
    }

    public BulletParent GetBulletParent()
    {
        return _bulletParent;
    }

    public void SetBulletParent(BulletParent newParent)
    {
        _bulletParent = newParent;
    }
}

public enum BulletParent
{
    Player,
    Enemy
}