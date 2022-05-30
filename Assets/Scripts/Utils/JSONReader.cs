using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PotionSizer
{
    public PotionData[] Potions;
}

public class JSONReader : MonoBehaviour
{
    [SerializeField] private TextAsset _potionClassificateJSON;

    public PotionSizer _potionSizer;

    private void Awake()
    {
        _potionSizer = new PotionSizer();
        _potionSizer = JsonUtility.FromJson<PotionSizer>(_potionClassificateJSON.text);
    }
}
 