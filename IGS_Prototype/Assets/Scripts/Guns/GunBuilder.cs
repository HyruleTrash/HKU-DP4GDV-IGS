using System;
using LucasCustomClasses;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "GunBuilder", menuName = "FPS/Guns/GunBuilder")]
public class GunBuilder : ScriptableObject
{
    [Serializable]
    public class FloatDeviationRecord
    {
        public bool useRandom;
        public float maximumValue;
        public float minimumValue;
    }
    
    [SerializeField] private float fireRate;
    [SerializeField] private float reloadTime;
    [SerializeField] private float baseDamage;
    [SerializeField] private int ammoCapacity;
    [SerializeField] private ScriptableObject shootStrategy;
    [SerializeField] private Vector3 bulletOrigin;
    [Header("Random deviations")]
    [SerializeField] private FloatDeviationRecord fireRateDeviation;
    [SerializeField] private FloatDeviationRecord reloadTimeDeviation;
    [SerializeField] private FloatDeviationRecord baseDamageDeviation;
    [Header("For projectile based guns")]
    [SerializeField] private float force = 0;

    private void OnValidate()
    {
        if (shootStrategy == null)
            return;
        if (shootStrategy is not IShootStrategy strategy)
            shootStrategy = null;
    }

    public Gun Build()
    {
        Gun newGun = new Gun(ammoCapacity, GetWithDeviation(baseDamage, baseDamageDeviation, f => f >= 0.1), force);
        newGun.fireRateTimer = new Timer(GetWithDeviation(fireRate, fireRateDeviation, f => f >= 0.01f));
        newGun.reloadTimer = new Timer(GetWithDeviation(reloadTime, reloadTimeDeviation, f => f >= 0.01f));
        newGun.shootStrategy = (IShootStrategy)shootStrategy;
        newGun.bulletOrigin = bulletOrigin;
        return newGun;
    }

    public float GetWithDeviation(float basis, FloatDeviationRecord record, Func<float, bool> condition)
    {
        if (!record.useRandom)
            return basis;
        
        float result = basis * Random.Range(record.minimumValue, record.maximumValue);
        Debug.Log($"result: {result}, condition: {condition(result)}, min: {record.minimumValue}");
        if (condition(result))
            return result;
        return record.minimumValue;
    }
}