using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PotionSizer
{
    public Potion[] Potion;
}

public class JSONReader : MonoBehaviour
{
    [SerializeField] private TextAsset _potionClassificateJSON;

    public PotionSizer _potionSizer = new PotionSizer();

    private void Awake()
    {
        _potionSizer = JsonUtility.FromJson<PotionSizer>(_potionClassificateJSON.text);
    }
}
 