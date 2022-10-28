using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GlobalTask2 : GlobalTask
{
    [SerializeField] private IngredientData _barkIngredient;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Money _money;

    private int _taskValue;
    private int _minMoneyValue;
    private float _inventoryTaskValue = 15;

    private bool _moneyComplete;
    private bool _inventoryComplete;

    public override void Init()
    {
        ChangeCostIngredient();

        Money.OnMoneyChanged.AddListener(CheckMoneyTask);
        Money.OnMoneyChanged.AddListener(CheckMoneyDefeat);

        Inventory.OnItemValueChanged += CheckInventory;
    }

    public override void SpecialSelection()
    {
        ChangeCostIngredient();
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

    private void ChangeCostIngredient()
    {
        _barkIngredient.cost *= 2;
    }


    private void CheckInventory()
    {
        foreach (KeyValuePair<IngredientData, int> item in _inventory.InventoryAmount)
        {
            if(item.Key.resourceRarity == ResourceRarity.rare)
            {
                if(item.Value >= _inventoryTaskValue)
                {
                    _inventoryComplete = true;
                    print("inventory complete");
                }
                else
                {
                    _inventoryComplete = false;
                    print("inventory fail");
                }
            }
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
