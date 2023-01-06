using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildRepCalculator
{
    private float _commonReward = 5;
    private float _rareReward = 10;
    private float _commonPenalty = 3;
    private float _rarePenalty = 8;

    public float CalculateReward(ResourceRarity rarity)
    {
        if(rarity == ResourceRarity.common)
        {
            return _commonReward;
        }
        else
        {
            return _rareReward;
        }
    }

    public float CalculatePenalty(ResourceRarity rarity)
    {
        if(rarity == ResourceRarity.common)
        {
            return _commonPenalty;
        }
        else
        {
            return _rarePenalty;
        }
    }
}
