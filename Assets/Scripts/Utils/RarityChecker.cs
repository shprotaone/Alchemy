using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RarityChecker
{
    public const string rareRarity = "rare";
    private const string commonRarity = "common";

    public ResourceRarity RarityCheck(string value)
    {
        if (value == commonRarity)
        {
            return ResourceRarity.common;
        }
        else
        {
            return ResourceRarity.rare;
        }
  
    }
}
