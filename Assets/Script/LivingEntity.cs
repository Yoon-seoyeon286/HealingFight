using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour,IDamageable
{
    public float DefaultHealth = 100.0f;
    public float health { get; protected set; }
    public bool dead { get; protected set; }
    public event Action onDeath;

    //생명체 활성화 될 때
    protected virtual void OnEnable()
    {
        dead = false;
        health = DefaultHealth;
    }

    //대미지
    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        health-= damage;
        if(health <= 0 && !dead)
        {
            Die();
        }
    }

    //체력 회복
    public virtual void RestoreHealth(float newHealth)
    {
        if (dead) return;

        health = newHealth; 
    }

    //사망 처리
    public virtual void Die()
    { 
        if(onDeath != null) onDeath();

        dead = true;
    }
}
