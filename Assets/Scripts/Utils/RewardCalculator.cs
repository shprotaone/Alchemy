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

    public void CalculateBase(GuildsType guildType,ResourceRarity rarity)
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

    public float GetReward(int count)
    {
        if (count == 1)
        {
            _reward = 100;
        }
        else if (count == 2)
        {
            _reward = 200;
        }
        else if (count == 3) 
        { 
            _reward = 300; 
        } 
        else
        {
            Debug.LogError("Значков нет");
            _reward = 0;
        }

        return _reward;
    }

    public float CalculateResult(int labelsCount,int matchIndex)
    {
        int delta = 0;       

        if(matchIndex != 0)
        {
            delta = labelsCount - matchIndex;
        }
        else
        {
            return 0;
        }
        
        if (delta == 0)
        {
            return Reward;
        } 
        else if(delta == 1)
        {
            return Reward * 0.3f;
        }
        else if(delta == 2)
        {
            return Reward * 0.5f;
        }
        else
        {
            return 0;
        }
    }
}
