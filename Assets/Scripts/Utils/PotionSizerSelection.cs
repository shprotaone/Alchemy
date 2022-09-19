using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSizerSelection
{
    private PotionSizer _fullSizer;
    private PotionSizer _resultSizer;

    public PotionSizerSelection(PotionSizer fullSizer)
    {
        _fullSizer = fullSizer;
        _resultSizer = new PotionSizer();
    }

    public PotionSizer SizerSelector(LevelNumber levelNumber)
    {
        if(levelNumber == LevelNumber.Level1)
        {
            SetCommonPotionSizer();
            return _resultSizer;
        }
        else if (levelNumber == LevelNumber.Level2)
        {
            SetSizerOnThirdIngredient();
            return _resultSizer;
        }
        else
        {
            SetCommonPotionSizer();
            return _fullSizer;
        }
    }

    private void SetCommonPotionSizer()
    {
        List<PotionData> result = new List<PotionData>();

        foreach (var item in _fullSizer.Potions)
        {
            item.SetIngredients();

            if (item.rarity == ResourceRarity.common.ToString())
            {
                result.Add(item);
            }
        }

        _resultSizer.Potions = result.ToArray();
    }

    private void SetSizerOnThirdIngredient()
    {
        List<PotionData> result = new List<PotionData>();

        foreach (var item in _fullSizer.Potions)
        {
            item.SetIngredients();

            if (item.ingredients.Count <= 3)
            {
                result.Add(item);
            }
        }

        _resultSizer.Potions = result.ToArray();
    }
}
