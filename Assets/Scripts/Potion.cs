using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    private string _name;

    private string[] _ingredients;
    private string _guild;
    private string _rarity;
    private EffectController _effectController;

    private GuildsType _guildType;
    public GuildsType GuildsType => _guildType;
    public ResourceRarity Rarity => SetRarity(_rarity);
    public string PotionName => _name;
    public string[] Ingredients => _ingredients;

    private void Start()
    {
        _ingredients = new string[5];
        _effectController = GetComponentInChildren<EffectController>(); //остановился тут
    }

    public void FillPotion(PotionData potionData)
    {
        _name = potionData.name;
        _guild = potionData.guild;
        _rarity = potionData.rarity;

        _ingredients = new string[5];
        FillIngredients(potionData.firstIngredient, potionData.secondIngredient, potionData.threeIngredient, potionData.fourIngredient, potionData.fiveIngredient);

    }

    public void FillPotion(Ingredient[] ingredients)
    {
        _name = name;
        _ingredients = new string[ingredients.Length];

        for (int i = 0; i < ingredients.Length; i++)
        {
            _ingredients[i] = IngredientsToString(ingredients[i]);
        }
    }

    public void SetNamePotion(string name)
    {
        _name = name;
    }

    public void FillIngredients(string firstIngredient,string secondIngredient,string thirdIngredient,
                                string fourIngredient,string fiveIngredient)
    {
        _ingredients[0] = firstIngredient;
        _ingredients[1] = secondIngredient;
        _ingredients[2] = thirdIngredient;
        _ingredients[3] = fourIngredient;
        _ingredients[4] = fiveIngredient;
    }

    public void SetGuild(string guild)
    {
        GuildChecker guildChecker = new GuildChecker();
        _guildType = guildChecker.GuildCheck(guild);
    }

    public void SetGuild(GuildsType guild)
    {
        _guildType = guild;
    }

    public ResourceRarity SetRarity(string rarity)
    {
        RarityChecker rarityChecker = new RarityChecker();
        return rarityChecker.RarityCheck(rarity);
    }

    private string IngredientsToString(Ingredient ingredient)
    {
        if (ingredient != null)
        {
            return ingredient.IngredientData.resourceType.ToString();
        }
        else
        {
            return "*";
        }
    }
}
