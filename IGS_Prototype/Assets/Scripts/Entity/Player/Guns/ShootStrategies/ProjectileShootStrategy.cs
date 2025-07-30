using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileShootStrategy", menuName = "FPS/Guns/ShootStrategies/ProjectileShootStrategy")]
public class ProjectileShootStrategy : ScriptableObject, IShootStrategy
{
    public void Shoot(ShootData data)
    {
        throw new System.NotImplementedException();
    }
}
