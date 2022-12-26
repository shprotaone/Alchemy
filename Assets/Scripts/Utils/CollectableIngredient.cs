using System.Collections.Generic;

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

        Inventory.OnItemValueChanged += CheckInventory;

    }

    private void SubscribeToSlots()
    {
        foreach (Slot slot in _inventory.Slots)
        {
            //slot.OnChangeValueWithIngredient += F;
        }
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
        foreach (Slot item in _inventory.Slots)
        {

            //if (item.IngredientInSlot.resourceRarity == ResourceRarity.common && _commonIngredientValue > 0)
            //{
            //    if (item.AmountInSlot >= _commonIngredientValue)
            //    {
            //        _proggressInInventory.Remove(item.IngredientInSlot);
            //        return;
            //    }
            //}
            //else if(item.IngredientInSlot.resourceRarity == ResourceRarity.rare && _rareIngredientValue > 0)
            //{
            //    if(item.AmountInSlot >= _rareIngredientValue)
            //    {
            //        _proggressInInventory.Remove(item.IngredientInSlot);
            //        return;
            //    }
            //}
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
