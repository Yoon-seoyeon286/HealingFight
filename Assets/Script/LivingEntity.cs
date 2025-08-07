using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour,IDamageable
{
    public float DefaultHealth = 100.0f;
    public float health { get; protected set; }
    public bool dead { get; protected set; }
    public event Action onDeath;

    //����ü Ȱ��ȭ �� ��
    protected virtual void OnEnable()
    {
        dead = false;
        health = DefaultHealth;
    }

    //�����
    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        health-= damage;
        if(health <= 0 && !dead)
        {
            Die();
        }
    }

    //ü�� ȸ��
    public virtual void RestoreHealth(float newHealth)
    {
        if (dead) return;

        health = newHealth; 
    }

    //��� ó��
    public virtual void Die()
    { 
        if(onDeath != null) onDeath();

        dead = true;
    }
}
