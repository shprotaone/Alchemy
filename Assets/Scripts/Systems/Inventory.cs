using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{    
    [SerializeField] private GameObject _currentIngredientPrefab;
    [SerializeField] private GameObject _prefabBottle;

    [SerializeField] private Transform _parentDragableObject;
    [SerializeField] private IngredientData[] _ingredients;
    [SerializeField] private TableManager _tableManager;

    private Dictionary<IngredientData, int> _inventory;

    private Slot[] _slots;

    public GameObject CurrentPrefab => _currentIngredientPrefab;
    public Transform ParentDragableObject => _parentDragableObject;
    public IngredientData[] Ingredients => _ingredients;

    void Start()
    {
        TutorialSystem.OnEndedTutorial += StartGameFilling;
        _slots = GetComponentsInChildren<Slot>();
        _inventory = new Dictionary<IngredientData, int>();        
    }

    public void FillInventory(int amount)
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

    private void ClearIngredientsInInventory()
    {
        _inventory.Clear();
    }

    private void StartGameFilling(bool flag)
    {
        if (flag)
        {
            ClearIngredientsInInventory();
            FillInventory(5);  //hardcode это плохо
            TutorialSystem.OnEndedTutorial -= StartGameFilling;
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
}
