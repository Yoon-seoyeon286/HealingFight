using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum State
    {
        Ready,
        Empty,
        Reloading
    }

    public State state { get; private set; }
    public Transform fireTransform;

    public ParticleSystem muzzleFlashEffect; //�ѱ� ȭ�� ȿ��

    AudioSource gunAudioSource;

    LineRenderer bulletLineRender;

    public GunData gunData;

    float fireDistance = 50f;

    public int ammoRemain = 100; // �����ִ� ��ü ź��
    public int magAmmo; // ���� źâ�� ���� �ִ� ź��

    float lastFireTime;


    private void Awake()
    {
        gunAudioSource = GetComponent<AudioSource>();
        bulletLineRender = GetComponent<LineRenderer>();

        bulletLineRender.positionCount = 2;
        bulletLineRender.enabled = false;
    }

    private void OnEnable()
    {
        ammoRemain = gunData.startAmmoRemain;
        magAmmo = gunData.magCapacity;

        state = State.Ready;
        lastFireTime = 0;
    }

    IEnumerator ShotEffect(Vector3 hitposition)
    {
        muzzleFlashEffect.Play();
        gunAudioSource.PlayOneShot(gunData.shotClip);

        bulletLineRender.SetPosition(0, fireTransform.position);
        bulletLineRender.SetPosition(1, hitposition);
        bulletLineRender.enabled = true;

        yield return new WaitForSeconds(0.03f);

        bulletLineRender.enabled = false;

    }

    public void Fire()
    {
        if (state == State.Ready && Time.time >= lastFireTime + gunData.timeBetFire)
        {
            lastFireTime = Time.time;
            Shot();
        }
    }

    void Shot()
    {
        RaycastHit hit;
        Vector3 hitPosition = Vector3.zero;

        //����ĳ��Ʈ(���� ����, ����, �浹 ���� �����̳�, �����Ÿ�)
        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit, fireDistance))
        {
            IDamageable target = hit.collider.GetComponent<IDamageable>();

            if (target != null)
            {
                target.onDamage(gunData.damage, hit.point, hit.normal);
            }

            //���� �浹 ��ġ ����
            hitPosition = hit.point;
        }

        else
        {
            //ź���� �ִ� �����Ÿ����� ���ư��� ���� ��ġ�� �浹 ��ġ�� ���
            hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
        }

        StartCoroutine(ShotEffect(hitPosition));

        magAmmo--;
        if (magAmmo <= 0)
        {
            state = State.Empty;
        }
    }

    public bool Reload()
    {
        if (state == State.Reloading || ammoRemain <= 0 || magAmmo >= gunData.magCapacity)
        {
            return false;
        }

        StartCoroutine(ReloadRoutine());
        {
            return true;
        }
   
 
    }

    IEnumerator ReloadRoutine()
    {
        state= State.Reloading;
        gunAudioSource.PlayOneShot(gunData.reloadClip);

        yield return new WaitForSeconds(gunData.reloadTime);

        int ammoToFill = gunData.magCapacity - magAmmo;

        if (ammoRemain < ammoToFill)
        {
            ammoToFill = ammoRemain;
        }

        magAmmo += ammoToFill;
        ammoRemain -= ammoToFill;


        state = State.Ready;

    }
}
