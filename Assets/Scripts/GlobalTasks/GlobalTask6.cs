using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTask6 : GlobalTask
{
    [SerializeField] private float _costMultiple = 0.9f;
    [SerializeField] private float _rewardMultiply = 1.05f;
    [SerializeField] private int _inventoryCommonTaskValue = 15;
    [SerializeField] private int _inventoryRareTaskValue = 2;
    [SerializeField] private int _timeDecreaseReputation = 30;
    [SerializeField] private int _reduceValue = 5;

    [SerializeField] private IngredientData[] _ingredientWithChangeCost;
    [SerializeField] private PotionTaskSystem _taskSystem;
    [SerializeField] private ReputationReducer _reputationReducer;
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
        _reputationReducer.InitReducer(_reduceValue, _timeDecreaseReputation);
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
                if (item.Value >= _inventoryCommonTaskValue)
                {
                    _proggressInInventory[item.Key] = true;
                }
            }

            if (item.Key.resourceRarity == ResourceRarity.rare)
            {
                if (item.Value >= _inventoryRareTaskValue)
                {
                    _proggressInInventory[item.Key] = true;
                }
            }
        }

        if (_proggressInInventory.Count == 8)
        {
            _inventoryComplete = true;
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
    private void SaveStockCost()
    {
        _stockCost = new int[_ingredientWithChangeCost.Length];

        for (int i = 0; i < _ingredientWithChangeCost.Length; i++)
        {
            _stockCost[i] = _ingredientWithChangeCost[i].cost;
        }
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
        _taskSystem.SetRewardMultiply(_rewardMultiply);
    }


    private void OnDisable()
    {
        for (int i = 0; i < _ingredientWithChangeCost.Length; i++)
        {
            _ingredientWithChangeCost[i].cost = _stockCost[i];
        }
    }
}
