using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static event Action OnItemValueChanged;

    [SerializeField] private IngredientData[] _ingredients;
    [SerializeField] private TableManager _tableManager;

    [SerializeField] private Shelf _commonShelf;
    [SerializeField] private Shelf _rareShelf;

    private List<Slot> _slots;

    public List<Slot> Slots => _slots;
    public IngredientData[] Ingredients => _ingredients;
    public int BottleCount => _tableManager.EmptyPotionTable.transform.childCount;

    public void InitInventory()
    {
        _slots = new List<Slot>();

        InitSlots(_commonShelf);
        InitSlots(_rareShelf);
    }

    private void InitSlots(Shelf shelf)
    {
        for (int i = 0; i < shelf.Slots.Length; i++)
        {
            _slots.Add(shelf.Slots[i]);
        }
    }

    /// <summary>
    /// Раздача стартовых простых ресурсов
    /// </summary>
    /// <param name="amount"></param>
    public void FillCommonIngredients(int amount)
    {
        for (int i = 0; i < _ingredients.Length; i++)
        {
            _slots[i].FillSlot(_ingredients[i]);
            _slots[i].SetStartValue(amount);
        }
    }

    public void StartGameFilling(bool secondFilling)
    {
        if (secondFilling)
        {
            foreach (var item in _slots)
            {
                item.SetStartValue(5);
            }
        }        
    }

    public void AddIngredient(IngredientData ingredient)
    {
        foreach (var item in _slots)
        {
            if(item.IngredientInSlot == ingredient)
            {
                item.IncreaseAmount();
                return;
            }
        }

        OnItemValueChanged?.Invoke();
    }

    public int ShowIngredientValue(IngredientData ingredient)
    {
        foreach (var item in _slots)
        {
            if (item.IngredientInSlot == ingredient)
            {
                return item.AmountInSlot;
            }
        }

        return 0;
    }

    public void HideRareShelf()
    {
        _rareShelf.HideShelf();
    }
}