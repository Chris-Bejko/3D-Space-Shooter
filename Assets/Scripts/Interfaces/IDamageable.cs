public interface IDamageable 
{
    public void TakeDamage(int damage);

    public void Destruct();

    public int GetHealth();

    public BulletParent GetBulletParent();

    public void SetBulletParent(BulletParent parent);
}
