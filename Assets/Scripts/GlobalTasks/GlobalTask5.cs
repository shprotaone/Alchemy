using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTask5 : GlobalTask
{
    [SerializeField] private float _costMultiple = 1.15f;
    [SerializeField] private float _rewardMultiply = 0.9f;
    [SerializeField] private int _inventoryTaskValue = 15;

    [SerializeField] private IngredientData[] _ingredientWithChangeCost;
    [SerializeField] private PotionTaskSystem _taskSystem;
    [SerializeField] private Inventory _inventory;

    [SerializeField] private Money _money;

    private Dictionary<IngredientData, bool> _proggressInInventory;

    private int[] _stockCost;
    private int _taskValue;
    private int _minMoneyValue;
    private bool _inventoryComplete;

    public override void Init()
    {
        _proggressInInventory = new Dictionary<IngredientData, bool>();

        SaveStockCost();
        ChangeCostIngredient();

        Money.OnMoneyChanged.AddListener(CheckMoneyTask);
        Money.OnMoneyChanged.AddListener(CheckMoneyDefeat);

        Inventory.OnItemValueChanged += CheckInventory;

        SpecialSelection();        
    }

    public override void CheckMoneyTask()
    {
        if (_taskValue <= _money.CurrentMoney && _inventoryComplete)
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
    }

    private void CheckInventory()
    {
        foreach (KeyValuePair<IngredientData, int> item in _inventory.InventoryAmount)
        {
            if (item.Key.resourceRarity == ResourceRarity.common)
            {
                if (item.Value >= _inventoryTaskValue)
                {
                    _proggressInInventory[item.Key] = true;
                }
            }
        }

       if(_proggressInInventory.Count == 4)
        {
            _inventoryComplete = true;
        }
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
