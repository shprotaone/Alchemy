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

    private string[] _ingredients;

    public string[] Ingredients => _ingredients;
    public GuildsType GuildsType => SetGuild();
    public ResourceRarity Rarity => SetRarity();

    public void FillPotion(Ingredient[] ingredients)
    {        
        firstIngredient = CheckNull(ingredients[0]);
        secondIngredient = CheckNull(ingredients[1]);
        threeIngredient = CheckNull(ingredients[2]);
        fourIngredient = CheckNull(ingredients[3]);
        fiveIngredient = CheckNull(ingredients[4]);

        _ingredients = new string[ingredients.Length];
        for (int i = 0; i < ingredients.Length; i++)
        {
            _ingredients[i] = CheckNull(ingredients[i]);
        }
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

    private GuildsType SetGuild()
    {
        GuildChecker guildChecker = new GuildChecker();
        return guildChecker.GuildCheck(this);
    }

    private ResourceRarity SetRarity()
    {
        RarityChecker rarityChecker = new RarityChecker();
        return rarityChecker.RarityCheck(this);
    }
}
