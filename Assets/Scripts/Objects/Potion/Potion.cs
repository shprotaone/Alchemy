using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Potion
{
    private ObjectType _effectType;
    private ResourceRarity _rarityType;   

    private List<string> _ingredients;
    private string _name;
    private bool _contraband;

    private GuildsType _guildType;

    public GuildsType GuildsType => _guildType;
    public ResourceRarity Rarity => _rarityType;
    public string PotionName => _name;
    public List<string> Ingredients => _ingredients;
    public ObjectType EffectType => _effectType;
    public bool Contraband => _contraband;

    public void FillPotion(PotionData potionData)
    {
        _ingredients = new List<string>();

        _name = potionData.name;
        SetGuild(potionData.guild);
        SetRarity(potionData.rarity);

        _ingredients = potionData.ingredients;
    }

    public void FillPotion(List<Ingredient> ingredients)        //посмотреть для чего перевод в массив string
    {
        _ingredients = new List<string>();

        for (int i = 0; i < ingredients.Count; i++)
        {
            _ingredients.Add(IngredientsToString(ingredients[i]));           
        }
    }

    public void SetNamePotion(string name)
    {
        _name = name;
    }

    public void SetEffect(List<Ingredient> ingredients)
    {
        foreach (var item in ingredients)
        {
            if (item != null)
            {
                if (item.IngredientData.resourceRarity == ResourceRarity.rare)
                {
                    _effectType = GetType(item.IngredientData.resourceType);
                    return;
                }
            }
        }
    }

    private ObjectType GetType(ResourceType resourceType)
    {
        return resourceType switch
        {
            ResourceType.Sand => ObjectType.EFFECT_SMOKE,
            ResourceType.Stone => ObjectType.EFFECT_FIRE,
            ResourceType.Ladan => ObjectType.EFFECT_BLINK,
            _ => ObjectType.EFFECT_SPARKS
        };
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
            return null;
        }
    }

    public bool PotionEquals(PotionData potionData)
    {
        Ingredients.Sort();

        potionData.ingredients.Sort();

        return Enumerable.SequenceEqual(Ingredients, potionData.ingredients);
    }

    public void SetContraband(bool isContraband)
    {
        _contraband = isContraband;
    }
}
