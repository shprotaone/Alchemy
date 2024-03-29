﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static event Action OnItemValueChanged;

    [SerializeField] private GameObject _prefabBottle;

    [SerializeField] private IngredientData[] _ingredients;
    [SerializeField] private TableManager _tableManager;

    [SerializeField] private Shelf _commonShelf;
    [SerializeField] private Shelf _rareShelf;

    private Dictionary<IngredientData, int> _inventory;

    private List<Slot> _slots;

    public IngredientData[] Ingredients => _ingredients;
    public Dictionary<IngredientData, int> InventoryAmount => _inventory;
    public int BottleCount => _tableManager.EmptyPotionTable.transform.childCount;

    private void Start()
    {
        _inventory = new Dictionary<IngredientData, int>();

        _slots = new List<Slot>();

        InitSlots(_commonShelf);
        InitSlots(_rareShelf);
    }


    /// <summary>
    /// Заполнение инвентаря
    /// </summary>
    public void InitInventory()
    {
        //_inventory = new Dictionary<IngredientData, int>();

        //_slots = new List<Slot>();

        //InitSlots(_commonShelf);
        //InitSlots(_rareShelf);
    }

    public void FillFullInventory(int amount)
    {
        for (int i = 0; i < _ingredients.Length; i++)
        {
            _inventory.Add(_ingredients[i], amount);
        }
        
        RefreshInventory();
    }

    /// <summary>
    /// Раздача стартовых простых ресурсов
    /// </summary>
    /// <param name="amount"></param>
    public void FillCommonIngredients(int amount)
    {
        for (int i = 0; i < _ingredients.Length; i++)
        {
            if (i < 4)
            {
                _inventory.Add(_ingredients[i], amount);
            }
            else
            {
                _inventory.Add(_ingredients[i], 0);
            }           
        }

        RefreshInventory();
    }

    private void RefreshInventory()
    {
        int count = 0;

        foreach (KeyValuePair<IngredientData, int> item in _inventory)
        {
            _slots[count].FillSlot(item.Key, item.Value);            
            count++;
        }

        OnItemValueChanged?.Invoke();
    }

    public void StartGameFilling(bool secondFilling)
    {
        if (secondFilling)
        {
            _inventory.Clear();
            FillFullInventory(0);

            for (int i = 0; i < 4; i++)
            {
                AddIngredient(_ingredients[i], 5);
            }

            RefreshInventory();
        }        
    }

    private void AddIngredient(IngredientData ingredient, int value)
    {
        _inventory[ingredient] = value;
    }

    public void AddIngredient(IngredientData ingredient)
    {
        _inventory[ingredient]++;
        RefreshInventory();
    }

    public void AddIngredientWithIndex(int index,int value)
    {
        _inventory[_ingredients[index]] += value;
        RefreshInventory();
    }

    public void DecreaseIngredient(IngredientData ingredient)
    {
        _inventory[ingredient]--;
        RefreshInventory();
    }

    public float ShowIngredientValue(IngredientData ingredient)
    {
        return _inventory[ingredient];
    }

    public bool DragFromInventory(IngredientData ingredient)
    {
        if (_inventory[ingredient] != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void InitSlots(Shelf shelf)
    {
        for (int i = 0; i < shelf.Slots.Length; i++)
        {
            _slots.Add(shelf.Slots[i]);
        }
    }

    public void HideRareShelf()
    {
        _rareShelf.HideShelf();
    }
}

//Система при раздельных бутылка
//public void AddBottle(int value)
//{
//    for (int i = 0; i < value; i++)
//    {
//        GameObject bottle = Instantiate(_prefabBottle, _tableManager.EmptyPotionTable.SetStartPosition(i), Quaternion.identity);
//        bottle.transform.SetParent(_tableManager.EmptyPotionTable.transform);
//    }

//    _tableManager.EmptyPotionTable.SortBottlePosition();
//    _tableManager.EmptyPotionTable.SortBottlePosition();
//}