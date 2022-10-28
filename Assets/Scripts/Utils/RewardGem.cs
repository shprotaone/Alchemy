using TMPro;
using UnityEngine;

public class RewardGem
{
    private const int range = 1500;
    public int RewardCounter { get; set; }

    public void CalculateReward(int completeCounter)
    {    
        if (completeCounter >= 15) RewardCounter = 20;
        else if (completeCounter >= 10) RewardCounter =  15;
        else if (completeCounter >= 5) RewardCounter = 10;
        else RewardCounter = 0;
    }

    public void CalculateFromMoney(int money)
    {
        if(money >= 0)
        {
            RewardCounter += 5;
        }
    }

    public void Penalty(int money)
    {
        int result = money;

        while(result <= 0)
        {
            result += range;
            RewardCounter--;
        }
    }
}
