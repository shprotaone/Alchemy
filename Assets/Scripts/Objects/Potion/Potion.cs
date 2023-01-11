using System.Collections.Generic;
using UnityEngine;

public class Potion
{
    private Color _color;

    public List<PotionLabelType> Labels { get; private set; }
    public Color ColorPotion => _color;
    public Potion() { }

    public Potion(List<PotionLabelType> labels)
    {
        Labels = new List<PotionLabelType>();
        Labels.AddRange(labels);
    }

    #region old
    //public void FillPotion(List<Ingredient> ingredients)        //заполнение для поиска
    //{
    //    _ingredients = new List<string>();

    //    for (int i = 0; i < ingredients.Count; i++)
    //    {
    //        _ingredients.Add(IngredientsToString(ingredients[i]));
    //    }
    //}
    //public void SetNamePotion(string name)
    //{
    //    _name = name;
    //}
    //public void SetColor(Color color)
    //{
    //    _color = color;
    //}
    //public void SetEffect(List<Ingredient> ingredients)
    //{
    //    foreach (var item in ingredients)
    //    {
    //        if (item != null)
    //        {
    //            if (item.IngredientData.resourceRarity == ResourceRarity.rare)
    //            {
    //                _effectType = GetType(item.IngredientData.resourceType);
    //                return;
    //            }
    //        }
    //    }
    //}

    //private ObjectType GetType(ResourceType resourceType)
    //{
    //    return resourceType switch
    //    {
    //        ResourceType.Sand => ObjectType.EFFECT_SMOKE,
    //        ResourceType.Stone => ObjectType.EFFECT_FIRE,
    //        ResourceType.Ladan => ObjectType.EFFECT_BLINK,
    //        _ => ObjectType.EFFECT_SPARKS
    //    };
    //}

    //public GuildsType SetGuild(string guild)
    //{
    //    GuildChecker guildChecker = new GuildChecker();
    //    return guildChecker.GuildCheck(guild);
    //}

    //public void SetGuild(GuildsType guild)
    //{
    //    _guildType = guild;
    //}

    //public void SetRarity(string rarity)
    //{
    //    RarityChecker rarityChecker = new RarityChecker();
    //    _rarityType = rarityChecker.RarityCheck(rarity);       
    //}

    //private string IngredientsToString(Ingredient ingredient)
    //{
    //    if (ingredient != null)
    //    {
    //        return ingredient.IngredientData.resourceType.ToString();
    //    }
    //    else
    //    {
    //        return null;
    //    }
    //}

    //public bool PotionEquals(PotionData potionData)
    //{
    //    Ingredients.Sort();

    //    potionData.ingredients.Sort();

    //    return Enumerable.SequenceEqual(Ingredients, potionData.ingredients);
    //}

    //public void SetContraband(bool isContraband)
    //{
    //    _contraband = isContraband;
    //}

    #endregion
}

