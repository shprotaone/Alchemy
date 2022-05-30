using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RarityChecker
{
    public const string rareRarity = "rare";
    private const string commonRarity = "common";

    public ResourceRarity RarityCheck(PotionData potionData)
    {
        if (potionData.rarity == commonRarity)
        {
            return ResourceRarity.Common;
        }
        else
        {
            return ResourceRarity.Rare;
        }
  
    }
}
