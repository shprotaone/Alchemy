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
        if (levelNumber == LevelNumber.Level1)
        {
            SetCommonPotionSizer();
            return _resultSizer;
        }
        else if (levelNumber == LevelNumber.Level2)
        {
            SetSizerOnThirdIngredient();
            return _resultSizer;
        }
        else if (levelNumber == LevelNumber.EndlessLevel)
        {
            SetFullSizer();
            return _fullSizer;
        }
        else if(levelNumber == LevelNumber.Level3a)
        {
            SetRangeSizerWithRandom(1);
            return _resultSizer;
        }
        else
        {
            return _fullSizer;
        }
    }

    private void SetCommonPotionSizer()
    {
        List<PotionData> result = new List<PotionData>();

        foreach (var item in _fullSizer.Potions)
        {            
            if (item.rarity == ResourceRarity.common.ToString())
            {
                result.Add(item);
                item.SetIngredients();
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

    private void SetFullSizer()
    {
        List<PotionData> result = new List<PotionData>();

        foreach (var item in _fullSizer.Potions)
        {
            item.SetIngredients();
        }

        _resultSizer.Potions = result.ToArray();
    }

    private void SetRangeSizerWithRandom(int range)
    {
        List<PotionData> result = new List<PotionData>();
        PotionData item;

        for (int i = 0; i < range; i++)
        {
            int numberPotion = Random.Range(0, _fullSizer.Potions.Length);
            item = _fullSizer.Potions[numberPotion];
            item.SetIngredients();  //�������� ������ 

            result.Add(item);
        }

        _resultSizer.Potions = result.ToArray();
    }
}
