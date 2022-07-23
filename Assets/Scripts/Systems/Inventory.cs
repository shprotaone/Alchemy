using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{    
    [SerializeField] private GameObject _currentIngredientPrefab;
    [SerializeField] private GameObject _prefabBottle;

    [SerializeField] private IngredientData[] _ingredients;
    [SerializeField] private TableManager _tableManager;

    private Dictionary<IngredientData, int> _inventory;

    private Slot[] _slots;

    public GameObject CurrentPrefab => _currentIngredientPrefab;
    public IngredientData[] Ingredients => _ingredients;
    public Dictionary<IngredientData, int> InventoryAmount => _inventory;

    private void Awake()
    {
        _slots = GetComponentsInChildren<Slot>();
    }
    void Start()
    {
        TutorialSystem.OnEndedTutorial += StartGameFilling;       
        _inventory = new Dictionary<IngredientData, int>();        
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
            GameObject bottle = Instantiate(_prefabBottle, _tableManager.EmptyPotionTable.SetStartPosition(),Quaternion.identity);
            bottle.transform.SetParent(_tableManager.EmptyPotionTable.transform);            
        }
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
            FillClearInventory(5);
        }        
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

    private void OnDisable()
    {
        TutorialSystem.OnEndedTutorial -= StartGameFilling;
    }
}
