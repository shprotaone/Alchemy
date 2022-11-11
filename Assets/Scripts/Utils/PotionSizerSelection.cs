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

    public PotionSizer SizerSelector(SizerType sizer)
    {
        switch (sizer)
        {
            case SizerType.FULL:
                SetFullSizer();
                return _resultSizer;
            case SizerType.TWICEINGREDIENTS:
                SetSizerWithIngredientCount(2);
                return _resultSizer;
            case SizerType.THIRDINGREDIENTS:
                SetSizerWithIngredientCount(3);
                return _resultSizer;
            case SizerType.COMMONINGREDIENTS:
                SetCommonPotionSizer();
                return _resultSizer;
            default:
                Debug.LogWarning("“ип по количеству ингредиентов не выбран");
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

    private void SetSizerWithIngredientCount(int ingredientsCount)
    {
        List<PotionData> result = new List<PotionData>();

        foreach (var item in _fullSizer.Potions)
        {
            item.SetIngredients();

            if (item.ingredients.Count <= ingredientsCount)
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
            result.Add(item);
        }

        _resultSizer.Potions = result.ToArray();
    }

    public PotionSizer SetRangeSizerWithRandom(int range)
    {
        List<PotionData> result = new List<PotionData>();
        PotionData item;

        if(range != 0)
        {
            for (int i = 0; i < range; i++)
            {
                int numberPotion = Random.Range(0, _resultSizer.Potions.Length);
                item = _resultSizer.Potions[numberPotion];
                item.SetIngredients();  //возможно лишнее 

                result.Add(item);
            }
            _resultSizer.Potions = result.ToArray();
        }
        else
        {
            Debug.LogWarning("”казан нулевой диапазон");
        }
       
        return _resultSizer;
    }
}
