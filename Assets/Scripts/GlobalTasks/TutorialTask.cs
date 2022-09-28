using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTask : GlobalTask
{
    [SerializeField] private Money _money;
    private int _taskValue;
    private int _minMoneyValue;

    public override void Init()
    {
        Money.OnMoneyChanged += CheckMoneyTask;
    }

    public override void CheckMoneyTask()
    {
        if (_taskValue <= _money.CurrentMoney)
        {
            _gameManager.CompleteLevel();
            OnLevelComplete?.Invoke();
        }
    }

    public override void SetTaskValue(int value, int minValue)
    {
        _taskValue = value;
        _minMoneyValue = minValue;
        SetLevelTaskText();
    }

    public override void SpecialSelection()
    {
        
    }

    public void OnDestroy()
    {
        Money.OnMoneyChanged -= CheckMoneyTask;
    }
}
