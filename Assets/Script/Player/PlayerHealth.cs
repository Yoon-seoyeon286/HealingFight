using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
    public Slider healthSlider;

    public AudioClip deathClip;
    public AudioClip hitClip;
    public AudioClip itemPickupClip;
    public AudioClip HealingClip;

    AudioSource audioSource;
    Animator animator;

    PlayerMove playerMove;
    PlayerShooter shooter;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        playerMove = GetComponent<PlayerMove>();
        shooter = GetComponent<PlayerShooter>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        healthSlider.gameObject.SetActive(true);
        healthSlider.maxValue = DefaultHealth;

        healthSlider.value = health;

        playerMove.enabled = true;
        shooter.enabled = true;
    }

    public override void RestoreHealth(float newHealth)
    {
        base.RestoreHealth(newHealth);
        healthSlider.value = health;
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (!dead)
        {
            audioSource.PlayOneShot(hitClip);
        }
        base.OnDamage(damage, hitPoint, hitNormal);
        healthSlider.value = health;
    }

    public override void Die()
    {
        base.Die();
        healthSlider.gameObject.SetActive(false);

        audioSource.PlayOneShot(deathClip);
        animator.SetTrigger("Die");

        playerMove.enabled=false;
        shooter.enabled=false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!dead)
        {
            
        }
    }
}
