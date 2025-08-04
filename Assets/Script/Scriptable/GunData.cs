using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    public AudioClip shotClip;
    public AudioClip reloadClip;

    public float damage = 20;

    public int startAmmoRemain = 100;
    public int magCapacity = 25; //źâ �뷮

    public float timeBetFire = 0.12f; //ź�� �߻� ����
    public float reloadTime = 1.8f; // ������ �ҿ�ð�

}
