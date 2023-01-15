using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelSetter
{
    private List<ResourceType> _types;
    private List<PotionLabelType> _labels = new List<PotionLabelType>();

    public List<PotionLabelType> Labels => _labels;
    public void SetTypeFromIngredient(List<Ingredient> ingredients)
    {
        _types = new List<ResourceType>();
        foreach (var ingredient in ingredients)
        {
            _types.Add(ingredient.ResourceType);
        }
    }

    public void Clear()
    {
        _types?.Clear();
        _labels?.Clear();
    }

    public List<PotionLabelType> GetCurrentLabels()
     {      
        List<ResourceType> tmpList = new List<ResourceType>();

        if (_types.Count == 2)
        {
            tmpList.Add(_types[0]);
            tmpList.Add(_types[1]);
            tmpList.Sort();

            _labels.Add(GetPotionLabel(tmpList[0],tmpList[1]));
        }

        if(_types.Count == 3)
        {
            tmpList.Clear();

            tmpList.Add(_types[1]);
            tmpList.Add(_types[2]);
            tmpList.Sort();

            _labels.Add(GetPotionLabel(tmpList[0], tmpList[1]));
        }

        if (_types.Count == 4)
        {
            tmpList.Clear();

            tmpList.Add(_types[2]);
            tmpList.Add(_types[3]);
            tmpList.Sort();

            _labels.Add(GetPotionLabel(tmpList[0], tmpList[1]));
        }

        return _labels;
    }

    public  PotionLabelType GetPotionLabel(ResourceType first, ResourceType second)
    {
        switch (first)
        {
            case ResourceType.White when second == ResourceType.Red:
                return PotionLabelType.FIRE;
            case ResourceType.White when second == ResourceType.Yellow:
                return PotionLabelType.ROCK;
            case ResourceType.White when second == ResourceType.Blue:
                return PotionLabelType.WATER;
            case ResourceType.Red when second == ResourceType.Yellow:
                return PotionLabelType.ROCK;
            case ResourceType.Red when second == ResourceType.Blue:
                return PotionLabelType.FIRE;
            case ResourceType.Yellow when second == ResourceType.Blue:
                return PotionLabelType.WATER;
            case ResourceType.White when second == ResourceType.White:
                return PotionLabelType.ROCK;
            case ResourceType.Red when second == ResourceType.Red:
                return PotionLabelType.FIRE;
            case ResourceType.Yellow when second == ResourceType.Yellow:
                return PotionLabelType.ROCK;
            case ResourceType.Blue when second == ResourceType.Blue:
                return PotionLabelType.WATER;

            default:
                Debug.LogError("Совпадение не найдено");
                return PotionLabelType.FIRE;
        }
    }
}
