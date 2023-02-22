using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarnMoneySubject : MonoBehaviour,ISubject
{
    private AchievementData _commonEarnAchieveData;
    private AchievementData _oneLoopEarnAchieveData;
    private AchievementData _oneDayEarnAchieveData;

    private void Start()
    {
        Money.OnChangeValueMoney += AddMoney;
        LevelInitializator.OnLoopRestart += ResetLoopAchieve;
        LevelInitializator.OnLevelStarted += ResetDayAchieve;
    }

    public void Init(List<AchievementData> achievmentsData)
    {
        foreach (var data in achievmentsData)
        {
            if (data.Id == AchieveID.COMMONEARNMONEY)
            {
                _commonEarnAchieveData = data;
            }
            else if(data.Id == AchieveID.EARNMONEYINLOOP)
            {
                _oneLoopEarnAchieveData = data;
            }
            else if(data.Id == AchieveID.EARNINONEDAY)
            {
                _oneDayEarnAchieveData = data;
            }
        }
    }

    private void AddMoney(int count)
    {
        _commonEarnAchieveData.IncreaseWithCount(count);
        _oneLoopEarnAchieveData.IncreaseWithCount(count);
    }

    private void ResetLoopAchieve()
    {
        if (!_oneLoopEarnAchieveData.Complete)
        {
            _oneLoopEarnAchieveData.GoalProgress = 0;
        }
    }

    private void ResetDayAchieve()
    {
        if (!_oneDayEarnAchieveData.Complete)
        {
            _oneDayEarnAchieveData.GoalProgress = 0;
        }
    }

    private void OnDestroy()
    {
        LevelInitializator.OnLevelStarted -= ResetDayAchieve;
        LevelInitializator.OnLoopRestart -= ResetLoopAchieve;
        Money.OnChangeValueMoney -= AddMoney;
    }
}
