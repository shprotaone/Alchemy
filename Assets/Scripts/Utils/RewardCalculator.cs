using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardCalculator
{    
    private int _allGuildsReward = 300;
    private int _knightCommonReward = 400;
    private int _knightRareReward = 700;
    private int _wizzardCommonReward = 400;
    private int _wizzardRareReward = 700;
    private int _banditCommonReward = 600;
    private int _banditRareReward = 900;
    private int _saintCommonReward = 300;
    private int _saintRareReward = 600;

    private float _reward;

    public float Reward => _reward;

    public void Calculate(GuildsType guildType,ResourceRarity rarity)
    {
        if(guildType == GuildsType.All)
        {
            _reward = _allGuildsReward;
        }
        else if (guildType == GuildsType.Saint && rarity == ResourceRarity.common)
        {
            _reward = _saintCommonReward;
        }
        else if (guildType == GuildsType.Saint && rarity == ResourceRarity.rare)
        {
            _reward = _saintRareReward;
        }
        else if (guildType == GuildsType.Bandit && rarity == ResourceRarity.common)
        {
            _reward = _banditCommonReward;
        }
        else if (guildType == GuildsType.Bandit && rarity == ResourceRarity.rare)
        {
            _reward = _banditRareReward;
        }
        else if (guildType == GuildsType.Wizzard && rarity == ResourceRarity.common)
        {
            _reward = _wizzardCommonReward;
        }
        else if (guildType == GuildsType.Wizzard && rarity == ResourceRarity.rare)
        {
            _reward = _wizzardRareReward;
        }
        else if (guildType == GuildsType.Knight && rarity == ResourceRarity.common)
        {
            _reward = _knightCommonReward;
        }
        else if (guildType == GuildsType.Knight && rarity == ResourceRarity.rare)
        {
            _reward = _knightRareReward;
        }
    }  
}
