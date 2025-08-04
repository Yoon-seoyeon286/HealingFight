using UnityEngine;

public interface IDamageable
{
    public void onDamage(float damage, Vector3 hitPoint, Vector3 hitNormal);
    
}
