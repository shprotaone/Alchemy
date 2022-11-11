using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableIngredient
{
    private LevelPreset _levelPreset;
    private Inventory _inventory;

    private int _commonIngredientValue;
    private int _rareIngredientValue;
    private int _progressCount;

    private bool _isActive;
    private bool _inventoryComplete;

    private Dictionary<IngredientData, bool> _proggressInInventory;

    public bool InventroryComplete => _inventoryComplete;
    public string Message { get; set; }

    public CollectableIngredient(LevelPreset levelPreset, Inventory inventory)
    {
        _proggressInInventory = new Dictionary<IngredientData, bool>();

        _levelPreset = levelPreset;
        _inventory = inventory;
        _commonIngredientValue = levelPreset.collectCommonResourceCount;
        _rareIngredientValue = levelPreset.collectRareResourceCount;

        SetProgressCount();
    }

    private void SetProgressCount()
    {     
        if(_commonIngredientValue > 0)
        {
            SetDictionary(ResourceRarity.common);
        }
        else if(_rareIngredientValue > 0)
        {
            SetDictionary(ResourceRarity.rare);
        }
        else if( _commonIngredientValue > 0 && _rareIngredientValue > 0)
        {
            SetDictionary(ResourceRarity.common);
            SetDictionary(ResourceRarity.rare);
        }

        Message = "Количество " + _progressCount + " " + "Активировано " + _isActive;
    }

    private void SetDictionary(ResourceRarity rarity)
    {
        foreach (var item in _inventory.Ingredients)
        {
            if (item.resourceRarity == rarity)
            {
                _proggressInInventory.Add(item,false);
            }
        }
    }

    private void FindResourceComplete()
    {
        foreach (KeyValuePair<IngredientData, int> item in _inventory.InventoryAmount)
        {
            if (item.Key.resourceRarity == ResourceRarity.common && _commonIngredientValue > 0)
            {
                if (item.Value >= _commonIngredientValue)
                {
                    _proggressInInventory.Remove(item.Key);
                }
            }
            else if(item.Key.resourceRarity == ResourceRarity.rare && _rareIngredientValue > 0)
            {
                if(item.Value >= _rareIngredientValue)
                {
                    _proggressInInventory.Remove(item.Key);
                    return;
                }
            }
        }
    }

    public void CheckInventory()
    {
        FindResourceComplete();

        if (_proggressInInventory.Count == 0)
        {
            _inventoryComplete = true;
            Message = "Complete";
        }
    }
}
