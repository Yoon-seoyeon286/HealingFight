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

    public ParticleSystem muzzleFlashEffect; //총구 화염 효과

    AudioSource gunAudioSource;

    LineRenderer bulletLineRender;

    public GunData gunData;

    float fireDistance = 50f;

    public int ammoRemain = 100; // 남아있는 전체 탄알
    public int magAmmo; // 현재 탄창에 남아 있는 탄알

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

        //레이캐스트(시작 지점, 방향, 충돌 정보 컨테이너, 사정거리)
        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit, fireDistance))
        {
            IDamageable target = hit.collider.GetComponent<IDamageable>();

            if (target != null)
            {
                target.onDamage(gunData.damage, hit.point, hit.normal);
            }

            //레이 충돌 위치 저장
            hitPosition = hit.point;
        }

        else
        {
            //탄알이 최대 사정거리까지 날아갔을 때의 위치를 충돌 위치로 사용
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
