using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public const int stockAmount = 5;
    public const int stockBottle = 2;

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
            GameObject bottle = Instantiate(_prefabBottle, _tableManager.EmptyPotionTable.SetPositionForBottle(),Quaternion.identity);
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
