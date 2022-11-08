using UnityEngine;
using TMPro;
using System;

public class GlobalTask1 : GlobalTask
{
    [SerializeField] private Money _money;
    //[SerializeField] private GuildSystem _guildSystem;

    private int _taskValue;
    private int _minMoneyValue;

    public override void Init()
    {
        Money.OnMoneyChanged.AddListener(CheckMoneyTask);
        Money.OnMoneyChanged.AddListener(CheckLevelDefeat);
    }

    public override void SpecialSelection()
    {
        
    }

    public override void CheckMoneyTask()
    {
        if (_taskValue <= _money.CurrentMoney)
        {
            _gameManager.CompleteLevel();
            OnLevelComplete?.Invoke();
        }
    }

    public override void SetTaskValue(int value,int minValue)
    {
        _taskValue = value;
        _minMoneyValue = minValue;
        SetLevelTaskText();
    }

    private void CheckLevelDefeat()
    {
        if(_minMoneyValue >= _money.CurrentMoney)
        {
            _gameManager.DefeatLevel();
        }
    }

    //private void OnDisable()
    //{
    //    Money.OnMoneyChanged -= CheckMoneyTask;
    //    Money.OnMoneyChanged -= CheckLevelDefeat;
    //}

}
