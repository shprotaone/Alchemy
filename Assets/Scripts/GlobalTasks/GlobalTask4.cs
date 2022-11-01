using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTask4 : GlobalTask
{
    [SerializeField] private float _costMultiple = 0.75f;
    [SerializeField] private float _rewardMultiply = 1.50f;
    [SerializeField] private float _penaltyMultyply = 1.50f;

    [SerializeField] private IngredientData[] _ingredientWithChangeCost;
    [SerializeField] private PotionTaskSystem _taskSystem;

    [SerializeField] private Money _money;

    private int[] _stockCost;
    private int _taskValue;
    private int _minMoneyValue;

    public override void Init()
    {
        SaveStockCost();
        ChangeCostIngredient();

        Money.OnMoneyChanged.AddListener(CheckMoneyTask);
        Money.OnMoneyChanged.AddListener(CheckMoneyDefeat);

        SpecialSelection();        
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
        ChangeLevelReward();
        ChangePenalty();
    }

    private void SaveStockCost()
    {
        _stockCost = new int[_ingredientWithChangeCost.Length];

        for (int i = 0; i < _ingredientWithChangeCost.Length; i++)
        {
            _stockCost[i] = _ingredientWithChangeCost[i].cost;
        }
    }

    private void ChangeCostIngredient()
    {
        foreach (var item in _ingredientWithChangeCost)
        {
            float result = item.cost * _costMultiple;
            item.cost = (int)result;
        }
    }

    private void ChangeLevelReward()
    {
        _taskSystem.SetRewardMultiply(_rewardMultiply);
    }

    private void ChangePenalty()
    {
        _taskSystem.SetPenaltyMultiply(_penaltyMultyply);
    }

    private void CheckMoneyDefeat()
    {
        if (_minMoneyValue >= _money.CurrentMoney)
        {
            _gameManager.DefeatLevel();
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _ingredientWithChangeCost.Length; i++)
        {
             _ingredientWithChangeCost[i].cost = _stockCost[i];
        }
    }
}
