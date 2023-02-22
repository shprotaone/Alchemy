using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private IngredientData[] _ingredients;
    [SerializeField] private Shelf _commonShelf;

    private List<Slot> _slots;

    public List<Slot> Slots => _slots;

    public void InitInventory()
    {
        _slots = new List<Slot>();

        InitSlots(_commonShelf);
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
}