using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class Potion
{
    public int number;
    public string name;

    public string firstIngredient;
    public string secondIngredient;
    public string threeIngredient;
    public string fourIngredient;
    public string fiveIngredient;

    public void FillPotion(Ingredient[] ingredients)
    {
        firstIngredient = CheckNull(ingredients[0]);
        secondIngredient = CheckNull(ingredients[1]);
        threeIngredient = CheckNull(ingredients[2]);
        fourIngredient = CheckNull(ingredients[3]);
        fiveIngredient = CheckNull(ingredients[4]);
    }

    private string CheckNull(Ingredient ingredient)
    {
        if(ingredient != null)
        {
            return ingredient.IngredientData.resourceType.ToString();
        }
        else
        {
            return "*";
        }
    }
}
