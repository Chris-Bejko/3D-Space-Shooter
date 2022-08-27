using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptables/EnemyData", order = 0)]
public class EnemyConfig : ScriptableObject
{
    public float MoveSpeed;

    public int Health;

    public int CollisionDamage;

    public float FiringDistance;

    public float FiringCooldown;
}
