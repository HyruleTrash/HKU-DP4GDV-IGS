using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RaycastShootStrategy", menuName = "FPS/Guns/ShootStrategies/RaycastShootStrategy")]
public class RaycastShootStrategy : ScriptableObject, IShootStrategy
{
    public void Shoot(ShootData data)
    {
        throw new NotImplementedException();
    }
}
