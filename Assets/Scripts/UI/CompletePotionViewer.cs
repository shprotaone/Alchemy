using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class CompletePotionViewer : MonoBehaviour
{
    [SerializeField] private Button _openButton;
    [SerializeField] private BottleInventory _bottleInventory;
    [SerializeField] private List<CompletePotionViewSlot> _slots;


    private List<BottleModel> _bottleList;

    private void Start()
    {
        _openButton.onClick.AddListener(FillCurrentBottles);
        _bottleList = new List<BottleModel>();
        LevelInitializator.OnLevelStarted += ClearSlots;
    }

    public void AddLastPotion(BottleModel potion)
    {
        _bottleList.Add(potion);
        RefreshsSlots();
    }

    private void RefreshsSlots()
    {
        int counter = 3;

        for(int i = 1; i <= _bottleList.Count; i++)
        {
            counter--;
            if (i < 4)
            {
                if (_bottleList.Count == 1) _slots[counter].SetSlot(_bottleList[0]);
                else _slots[counter].SetSlot(_bottleList[_bottleList.Count - i]);
            }
            else
            {
                return;
            }
        }
    }

    private void ClearSlots()
    {
        foreach (var slot in _slots)
        {
            slot.ClearSlot();
        }

        _bottleList.Clear();
    }

    private void FillCurrentBottles()
    {
        for (int i = 0; i < _bottleInventory.Slots.Count; i++)
        {
            FullBottleSlot slot = _bottleInventory.Slots[i];

            if (!slot.IsFree)
            {
                _slots[i].SetSlot(slot);
            }
            else
            {
                _slots[i].ClearSlot();
            }
        }
    }

    private void OnDisable()
    {
        LevelInitializator.OnLevelStarted -= ClearSlots;
    }
}
