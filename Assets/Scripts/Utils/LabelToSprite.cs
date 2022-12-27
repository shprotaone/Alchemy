using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LabelToSprite : MonoBehaviour
{
    [SerializeField] private List<Sprite> _sprites;

    public Sprite GetSprite(PotionLabelType label)
    {
        if(label == PotionLabelType.ROCK)
        {
            return _sprites[0];
        }
        else if(label == PotionLabelType.FIRE)
        {
            return _sprites[1];
        }
        else if(label == PotionLabelType.WATER)
        {
            return _sprites[2];
        }
        else
        {
            Debug.LogError("изображение не найдено");
            return null;
        }
    }
}
