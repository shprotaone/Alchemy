using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleInventory : MonoBehaviour
{
    public event Action<Potion> OnBottleAdded;

    [SerializeField] private List<FullBottleSlot> _slots;

    public FullBottleSlot GetFreeSlot()
    {
        foreach (var slot in _slots)
        {
            if (slot.IsFree)
            {              
                return slot;
            }
        }

        Debug.LogError("Нет свободных позиций");

        return null;
    }

    public void AddPotionInInventory(Potion potion)
    {
        OnBottleAdded?.Invoke(potion);
    }
}
