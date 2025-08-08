using UnityEngine;

[CreateAssetMenu(fileName = "SoldierData", menuName = "Scriptable Objects/SoldierData")]
public class SoldierData : ScriptableObject
{
    public float health = 100f;
    public float damage = 20f;
    public float speed = 2f;
}
