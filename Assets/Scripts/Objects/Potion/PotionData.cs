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

    public List<string> ingredients;

    public void SetIngredients()
    {
        ingredients = new List<string>();

        if (firstIngredient != null)
        {
            ingredients.Add(firstIngredient);
        }
        
        if (secondIngredient != null)
        {
            ingredients.Add(secondIngredient);
        }
        
        if (threeIngredient != null)
        {
            ingredients.Add(threeIngredient);           
        }
        
        if(fourIngredient != null)
        {
            ingredients.Add(fourIngredient);        
        }
        
        if(fiveIngredient != null)
        {
            ingredients.Add(firstIngredient);
        }     
    }
}
