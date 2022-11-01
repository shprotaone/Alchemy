using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GlobalTask3a : GlobalTask
{
    private const float rewardMultiply = 0.8f;

    [SerializeField] private Money _money;
    [SerializeField] private PotionTaskSystem _taskSystem;
    [SerializeField] private ContrabandPotionSystem _contrabandPotionSystem;

    [SerializeField] private int _timeToConrabandPotion;
    [SerializeField] private int _countToPotionSizer;

    private int _minMoneyValue;
    public int CountToPotionSizer => _countToPotionSizer;
    public int TimeToContrabandPotion => _timeToConrabandPotion;

    public override void Init()
    {
        Money.OnMoneyChanged.AddListener(CheckMoneyDefeat);
        LevelInitializator.OnInitComplete += SetGoalText;
        SpecialSelection();     
    }

    public override void CheckMoneyTask() { }

    public override void SetTaskValue(int value, int minValue)
    {
        _minMoneyValue = minValue;
        SetLevelTaskText();
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

    private void SetGoalText()
    {
        string text = "\n\n ŒÕ“–¿¡¿ÕƒÕŒ≈ «≈À‹≈ - " + _contrabandPotionSystem.ContrabandPotion.PotionName;

        _goalText += text;
        SetLevelTaskText();
    }
}
