using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelSetter
{
    private List<ResourceType> _types;

    public LabelSetter(List<Ingredient> ingredients)
    {
        _types = new List<ResourceType>();
        foreach (var ingredient in ingredients)
        {
            _types.Add(ingredient.ResourceType);
        }

        _types.Sort();
    }

    public List<PotionLabelType> GetCurrentLabels()
     {
         List<PotionLabelType> labels = new List<PotionLabelType>();

        if (_types.Count == 2)
        {
            labels.Add(GetPotionLabel(_types[0], _types[1]));
        }
        else if (_types.Count == 3)
        {
            labels.Add(GetPotionLabel(_types[0], _types[1]));
            labels.Add(GetPotionLabel(_types[1], _types[2]));
        }
        else if (_types.Count == 4)
        {
            labels.Add(GetPotionLabel(_types[0], _types[1]));
            labels.Add(GetPotionLabel(_types[1], _types[2]));
            labels.Add(GetPotionLabel(_types[2], _types[3]));
        }
        else
        {
            Debug.LogError("Не хватает ингредиентов для маркировки");
            return null;
        }

        return labels;
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
            case ResourceType.Yellow when second == ResourceType.Yellow:
                return PotionLabelType.WATER;
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
