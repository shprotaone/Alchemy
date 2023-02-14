using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Potion
{
    private Color _color;

    public List<PotionLabelType> Labels { get; private set; }
    public string LabelID { get; private set; }
    public Potion() { }

    public Potion(List<PotionLabelType> labels)
    {
        Labels = new List<PotionLabelType>();
        LabelID = "";
        Labels.AddRange(labels);
        Labels.Sort();

        foreach (var label in labels)
        {
            LabelID = LabelID.Insert(LabelID.Length, label.ToString());
        }
    }
}

