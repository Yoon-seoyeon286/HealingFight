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

    public State state {  get; private set; }
    public Transform fireTransform;

    public ParticleSystem muzzleFlashEffect; //ÃÑ±¸ È­¿° È¿°ú

    AudioSource gunAudioSource;

    LineRenderer bulletLineRender;

    public GunData gunData;

    float fireDistance = 50f;

    public int ammoRemain = 100; // ³²¾ÆÀÖ´Â ÀüÃ¼ Åº¾Ë
    public int magAmmo; // ÇöÀç ÅºÃ¢¿¡ ³²¾Æ ÀÖ´Â Åº¾Ë

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

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
