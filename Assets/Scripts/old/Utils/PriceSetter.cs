using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PriceSetter
{
    private List<IngredientData> _ingredients;

    private int[] _stockPrices;

    public PriceSetter(IngredientData[] ingredients)
    {
        _ingredients = ingredients.ToList();

        SavePrices();
    }

    public void ChangeCost(IngredientData[] ingredients, int multiply)
    {
        for (int i = 0; i < ingredients.Length; i++)
        {
            for (int j = 0; j < _ingredients.Count; j++)
            {
                if(ingredients[i] == _ingredients[j])
                {
                    _ingredients[j].cost *= multiply;
                }
            }
        }
    }

    private void SavePrices()
    {
        _stockPrices = new int[_ingredients.Count];

        for (int i = 0; i < _stockPrices.Length; i++)
        {
            _stockPrices[i] = _ingredients[i].cost;
        }
    }

    public void ResetPrice()
    {
        for (int i = 0; i < _ingredients.Count; i++)
        {
            _ingredients[i].cost = _stockPrices[i];
        }
    }
}
