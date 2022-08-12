using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{    
    [SerializeField] private GameObject _currentIngredientPrefab;
    [SerializeField] private GameObject _prefabBottle;

    [SerializeField] private IngredientData[] _ingredients;
    [SerializeField] private TableManager _tableManager;
    [SerializeField] private Shelf _commonShelf;
    [SerializeField] private Shelf _rareShelf;

    private Dictionary<IngredientData, int> _inventory;

    private List<Slot> _slots;

    public GameObject CurrentPrefab => _currentIngredientPrefab;
    public IngredientData[] Ingredients => _ingredients;
    public Dictionary<IngredientData, int> InventoryAmount => _inventory;
    public int BottleCount => _tableManager.EmptyPotionTable.transform.childCount;


    public void InitInventory()
    {
        TutorialSystem.OnEndedTutorial += StartGameFilling;       
        _inventory = new Dictionary<IngredientData, int>();

        _slots = new List<Slot>();

        InitSlots(_commonShelf);
        InitSlots(_rareShelf);
    }

    public void FillClearInventory(int amount)
    {
        for (int i = 0; i < _ingredients.Length; i++)
        {
            _inventory.Add(_ingredients[i], amount);
        }
        
        RefreshInventory();
    }

    public void AddBottle(int value)
    {
        for (int i = 0; i < value; i++)
        {
            GameObject bottle = Instantiate(_prefabBottle, _tableManager.EmptyPotionTable.SetStartPosition(i),Quaternion.identity);
            bottle.transform.SetParent(_tableManager.EmptyPotionTable.transform);            
        }

        _tableManager.EmptyPotionTable.SortBottlePosition();
        //_tableManager.EmptyPotionTable.SortBottlePosition();
    }

    private void RefreshInventory()
    {
        int count = 0;

        foreach (KeyValuePair<IngredientData, int> item in _inventory)
        {
            _slots[count].FillSlot(item.Key, item.Value);            
            count++;
        }
    }

    private void StartGameFilling(bool secondFilling)
    {
        if (secondFilling)
        {
            _inventory.Clear();
            FillClearInventory(0);

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

    private void OnDisable()
    {
        TutorialSystem.OnEndedTutorial -= StartGameFilling;
    }
}
