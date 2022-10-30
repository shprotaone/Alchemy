using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GlobalTask2a : GlobalTask
{
    private const float rewardMultiply = 0.8f;

    [SerializeField] private Money _money;
    [SerializeField] private PotionTaskSystem _taskSystem;
    [SerializeField] private ContrabandPotionSystem _contrabandPotionSystem;
    [SerializeField] private TMP_Text _contrabandCounter;
    
    private int _minMoneyValue;

    public override void Init()
    {
        Money.OnMoneyChanged.AddListener(CheckMoneyDefeat);
        SpecialSelection();
        SetGoalText();
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
        string text = "\nКонтрабандное зелье - " + _contrabandPotionSystem.ContrabandPotion.PotionName;

        _goalText += text;
    }
}
