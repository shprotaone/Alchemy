using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BottleInventory : MonoBehaviour
{
    public event Action<Potion> OnBottleAdded;

    [SerializeField] private TMP_Text _bottleCount;
    [SerializeField] private List<FullBottleSlot> _slots;

    private FullBottleSlot _currentSlot;
    private int _bottleInInventory = 0;
    public List<FullBottleSlot> Slots => _slots;

    public FullBottleSlot GetSlot(Potion potionInBottle)
    {
        _currentSlot = FindSlotWithPotion(potionInBottle);

        if(_currentSlot == null)
        {
            _currentSlot = FindFreeSlot(potionInBottle);
        }

        return _currentSlot;
    }

    private FullBottleSlot FindFreeSlot(Potion potion)
    {
        foreach (var slot in _slots)
        {
            if (slot.IsFree)
            {
                _currentSlot = slot;
                return _currentSlot;
            }
        }

        return null;
    }

    public FullBottleSlot FindSlotWithPotion(Potion potion)
    {
        foreach (var slot in _slots)
        {
            if(slot.BottleInSlot != null)
            {
                if (Enumerable.SequenceEqual(slot.BottleInSlot.Labels, potion.Labels))
                {                   
                    return slot;
                }
            }        
        }

        Debug.Log("������� ���� �� ������");
        return null;
    }

    public void AddPotionInInventory(Potion potion)
    {
        //OnBottleAdded?.Invoke(potion);
        _bottleInInventory++;
        RefreshText();
    }

    public void Decrease()
    {
        _bottleInInventory--;
        RefreshText();
    }

    public void RefreshText()
    {
        _bottleCount.text = _bottleInInventory.ToString();
    }
}
