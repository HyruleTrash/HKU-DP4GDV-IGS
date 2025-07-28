using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "LevelManagement/LevelData")]
public class LevelData : ScriptableObject
{
    [SerializeField] private Vector3 levelEndPosition;
    [SerializeField] private List<LevelDataEntity> entities;

    public void Load()
    {
        foreach (LevelDataEntity e in entities)
        {
            e.Load();
        }
    }
}
