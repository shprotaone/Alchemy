using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class PotionData
{
    public int number;
    public string name;

    public string firstIngredient;
    public string secondIngredient;
    public string threeIngredient;
    public string fourIngredient;
    public string fiveIngredient;
    public string guild;
    public string rarity;

    public string[] ingredients;
    //здесь можно перенести в массив прям в этом FillIngredient переносим сюда

    public void SetIngrediets()
    {
        ingredients = new string[5];

        ingredients[0] = firstIngredient;
        ingredients[1] = secondIngredient;
        ingredients[2] = threeIngredient;
        ingredients[3] = fourIngredient;
        ingredients[4] = fiveIngredient;
    }

}
