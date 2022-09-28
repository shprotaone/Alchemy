using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTask2a : GlobalTask
{
    private const float rewardMultiply = 0.8f;

    [SerializeField] private Money _money;
    [SerializeField] private PotionTaskSystem _taskSystem;
    
    private int _minMoneyValue;
    public override void Init()
    {
        Money.OnMoneyChanged += CheckMoneyDefeat;
    }

    public override void CheckMoneyTask() { }

    public override void SetTaskValue(int value, int minValue)
    {
        _minMoneyValue = minValue;
        
    }

    public override void SpecialSelection()
    {
        ChangeLevelReward();
    }

    private void CheckMoneyDefeat()
    {
        if (_minMoneyValue >= _money.CurrentMoney)
        {
            _gameManager.DefeatLevel();
        }
    }

    private void ChangeLevelReward()
    {
        _taskSystem.SetRewardMultiply(rewardMultiply);
    }

    private void OnDisable()
    {
        Money.OnMoneyChanged -= CheckMoneyDefeat;
    }
}
