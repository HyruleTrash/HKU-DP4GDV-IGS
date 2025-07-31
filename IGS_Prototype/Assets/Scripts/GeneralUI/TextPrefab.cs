using System;
using TMPro;
using UnityEngine;

[Serializable]
public class TextPrefab
{
    public GameObject prefab;
    [HideInInspector] public GameObject instance;
    [HideInInspector] public TextMeshProUGUI instanceTextComponent;

    public void Instantiate(Transform parent)
    {
        instance = GameObject.Instantiate(prefab, parent);
        instanceTextComponent = instance.GetComponent<TextMeshProUGUI>();
    }
}