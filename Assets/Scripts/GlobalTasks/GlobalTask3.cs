using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GlobalTask3 : GlobalTask
{
    [SerializeField] private IngredientData _barkIngredient;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Money _money;

    private int _taskValue;
    private int _minMoneyValue;
    private float _inventoryTaskValue = 15;

    private Dictionary<IngredientData, bool> _proggressInInventory;
    private bool _moneyComplete;
    private bool _inventoryComplete;

    public override void Init()
    {
        ChangeCostIngredient();

        Money.OnMoneyChanged.AddListener(CheckMoneyTask);
        Money.OnMoneyChanged.AddListener(CheckMoneyDefeat);

        Inventory.OnItemValueChanged += CheckInventory;

        _proggressInInventory = new Dictionary<IngredientData, bool>();
    }

    public override void SpecialSelection()
    {
        ChangeCostIngredient();
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

    private void ChangeCostIngredient()
    {
        _barkIngredient.cost *= 2;
    }


    private void CheckInventory()
    {
        foreach (KeyValuePair<IngredientData, int> item in _inventory.InventoryAmount)
        {
            if (item.Key.resourceRarity == ResourceRarity.rare)
            {
                if (item.Value >= _inventoryTaskValue)
                {
                    _proggressInInventory[item.Key] = true;
                }
            }
        }

        if (_proggressInInventory.Count == 4)
        {
            _inventoryComplete = true;
        }
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
        _barkIngredient.cost = 50;
    }
}
