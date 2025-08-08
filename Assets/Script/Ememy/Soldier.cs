using UnityEngine;
using UnityEngine.AI;

public class Soldier : LivingEntity
{
    public LayerMask Target;

    LivingEntity targetEnity;
    NavMeshAgent navMeshAgent;

    public ParticleSystem hitEffect;
    public AudioClip deathSound;
    public AudioClip hitSound;

    Animator animator;
    AudioSource audioSource;
    Renderer soldierRenderer;

    public float damage = 20f;
    public float timeAttack = 0.5f;
    float lastAttackTime;
    
    bool hasTarger
    {
        get
        {
            if (targetEnity != null && !targetEnity.dead)
            {
                return true;
            }

            return false;
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
