using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PotionForStock : ScriptableObject
{
    public List<PotionLabelType> labels;
    public bool isCooked;
}
