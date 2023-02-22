using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellBottleSubject : MonoBehaviour,ISubject
{
    [SerializeField] private TradeSlot _slot;
    [SerializeField] private TradeSystem _tradeSystem;

    private AchievementData _sellBottleAchieve;
    private AchievementData _declineVisitorAchieve;

    private void Start()
    {
        _slot.OnSell += IncreaseBottle;
        _tradeSystem.OnDecline += IncreaseDeclineVisitor;
    }
    public void Init(List<AchievementData> achievment)
    {
        foreach (var achievementData in achievment)
        {
            if (achievementData.Id == AchieveID.SELLPOTIONS)
            {
                _sellBottleAchieve = achievementData;
            }
            else if (achievementData.Id == AchieveID.DECLINEVISITOR)
            {
                _declineVisitorAchieve = achievementData;
            }
        }
    }

    private void IncreaseDeclineVisitor()
    {
        _declineVisitorAchieve.Increase();
    }
    private void IncreaseBottle(int count)
    {
        _sellBottleAchieve.IncreaseWithCount(count);
    }

    private void OnDestroy()
    {
        _tradeSystem.OnDecline -= IncreaseDeclineVisitor;
        _slot.OnSell -= IncreaseBottle;
    }
}
