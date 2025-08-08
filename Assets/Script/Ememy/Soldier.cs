using System.Collections;
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

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator    = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        soldierRenderer = GetComponentInChildren<Renderer>();
    }

    public void Setup(SoldierData soldierData)
    {
        DefaultHealth = soldierData.health;
        health = soldierData.health;
        damage = soldierData.damage;

        navMeshAgent.speed =soldierData.speed;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(UpdatePath());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator UpdatePath()
    {
        while (!dead)
        {
            if (hasTarger)
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(targetEnity.transform.position);
            }

            else
            {
                navMeshAgent.isStopped = true;

                Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, Target);

                for (int i = 0; i < colliders.Length; i++)
                {
                    LivingEntity livingEntity = colliders[i].GetComponent<LivingEntity>();
                    if (livingEntity != null && livingEntity.dead)
                    {
                        targetEnity = livingEntity;

                        break;
                    }
                }
            }
            yield return new WaitForSeconds(0.25f);
        }

    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (!dead)
        {
            hitEffect.transform.position = hitPoint;
            hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
            hitEffect.Play();

            audioSource.PlayOneShot(hitSound);
        }
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    public override void Die()
    {
        base.Die();

        Collider[] soldiercolliders = GetComponents<Collider>();
        for (int i = 0; i < soldiercolliders.Length; i++)
        {
            soldiercolliders[i].enabled = false;
        }

        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;

        animator.SetBool("Dead", true);
        audioSource.PlayOneShot(deathSound);


    }
}
