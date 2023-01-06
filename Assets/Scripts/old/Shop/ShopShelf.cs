using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopShelf : MonoBehaviour
{
    [SerializeField] private ShopSlot[] _slots;

    public void FillSlots(IngredientData[] ingredient)
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i].FillSlot(ingredient[i]);
        }
    }

    public void HideSlots(bool flag)
    {
        foreach (var slot in _slots)
        {
            slot.HideSlot(flag);
        }
    }

    public void HideSlotToIndex(int index,bool flag)
    {
        _slots[index].HideSlot(flag);
    }
}
