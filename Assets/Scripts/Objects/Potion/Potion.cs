using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    private GameObject _effect;
    private ResourceRarity _rarityType;
    private string _name;

    private string[] _ingredients;

    private GuildsType _guildType;
    public GuildsType GuildsType => _guildType;
    public ResourceRarity Rarity => _rarityType;
    public string PotionName => _name;
    public string[] Ingredients => _ingredients;
    public GameObject Effect => _effect;

    private void Start()
    {
        _ingredients = new string[5];
    }

    public void FillPotion(PotionData potionData)   ///заполнение из TaskSystem
    {
        _name = potionData.name;
        SetGuild(potionData.guild);
        SetRarity(potionData.rarity);

        _ingredients = new string[5];
        FillIngredients(potionData.firstIngredient, potionData.secondIngredient, potionData.threeIngredient, potionData.fourIngredient, potionData.fiveIngredient);
    }

    public void FillPotion(Ingredient[] ingredients)        //посмотреть для чего перевод в массив string
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

    public void SetEffect(Ingredient[] ingredients)
    {
        foreach (Ingredient item in ingredients)
        {
            if (item != null)
            {
                if (item.IngredientData.resourceRarity == ResourceRarity.rare)
                {
                    _effect = item.EffectPrefab;
                    return;
                }
            }
        }
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

    public void SetRarity(string rarity)
    {
        RarityChecker rarityChecker = new RarityChecker();
        _rarityType = rarityChecker.RarityCheck(rarity);
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

    public bool PotionEquals(PotionData potionData)
    {
        bool potionFind = (Ingredients[0] == potionData.firstIngredient
                            &&
                            Ingredients[1] == potionData.secondIngredient
                            &&
                            Ingredients[2] == potionData.threeIngredient
                            &&
                            Ingredients[3] == potionData.fourIngredient
                            &&
                            Ingredients[4] == potionData.fiveIngredient);
        return potionFind;
    }
}
