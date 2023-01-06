public class RewardCalculator
{
    private const float stockReward = 100;
    private float _reward;

    public float Reward => _reward;

    public float GetReward(int match)
    {
        if (match == 1)
        {
            _reward = stockReward;
        }
        else if (match == 2)
        {
            _reward = stockReward * 2.5f;
        }
        else if (match == 3) 
        { 
            _reward = stockReward * 6; 
        } 
        else
        {            
            _reward = 0;
        }

        return _reward;
    }
        
}

#region old
//public void CalculateBase(GuildsType guildType,ResourceRarity rarity)
//{
//    if(guildType == GuildsType.All)
//    {
//        _reward = _allGuildsReward;
//    }
//    else if (guildType == GuildsType.Saint && rarity == ResourceRarity.common)
//    {
//        _reward = _saintCommonReward;
//    }
//    else if (guildType == GuildsType.Saint && rarity == ResourceRarity.rare)
//    {
//        _reward = _saintRareReward;
//    }
//    else if (guildType == GuildsType.Bandit && rarity == ResourceRarity.common)
//    {
//        _reward = _banditCommonReward;
//    }
//    else if (guildType == GuildsType.Bandit && rarity == ResourceRarity.rare)
//    {
//        _reward = _banditRareReward;
//    }
//    else if (guildType == GuildsType.Wizzard && rarity == ResourceRarity.common)
//    {
//        _reward = _wizzardCommonReward;
//    }
//    else if (guildType == GuildsType.Wizzard && rarity == ResourceRarity.rare)
//    {
//        _reward = _wizzardRareReward;
//    }
//    else if (guildType == GuildsType.Knight && rarity == ResourceRarity.common)
//    {
//        _reward = _knightCommonReward;
//    }
//    else if (guildType == GuildsType.Knight && rarity == ResourceRarity.rare)
//    {
//        _reward = _knightRareReward;
//    }
//}
#endregion

