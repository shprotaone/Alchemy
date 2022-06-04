using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public const int stockAmount = 5;
    public const int stockBottle = 2;

    [SerializeField] private UnityEngine.GameObject _currentIngredientPrefab;
    [SerializeField] private UnityEngine.GameObject _prefabBottle;

    [SerializeField] private Transform _parentDragableObject;
    [SerializeField] private IngredientData[] _ingredients;
    [SerializeField] private Table _clearBottleTable;

    private Dictionary<IngredientData, int> _inventory;

    private Slot[] _slots;

    public UnityEngine.GameObject CurrentPrefab => _currentIngredientPrefab;
    public Transform ParentDragableObject => _parentDragableObject;
    public IngredientData[] Ingredients => _ingredients;
    void Start()
    {
        _slots = GetComponentsInChildren<Slot>();
        _inventory = new Dictionary<IngredientData, int>();
        AddBottle(stockBottle);
        FillInventory();
    }

    private void FillInventory()
    {
        for (int i = 0; i < _ingredients.Length; i++)
        {
            _inventory.Add(_ingredients[i], stockAmount);
        }

        RefreshInventory();
    }
    private void AddBottle(int value)
    {
        for (int i = 0; i < value; i++)
        {
            UnityEngine.GameObject bottle = Instantiate(_prefabBottle, _clearBottleTable.transform);
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

    public void AddIngredient(IngredientData ingredient)
    {
        _inventory[ingredient]++;
        RefreshInventory();
    }

    public void DecreaseIngredient(IngredientData ingredient,int value)
    {
        _inventory[ingredient] -= value;
        RefreshInventory();
    }
}
