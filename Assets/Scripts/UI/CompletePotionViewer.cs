using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompletePotionViewer : MonoBehaviour
{
    [SerializeField] private Button _openButton;
    [SerializeField] private BottleInventory _bottleInventory;
    [SerializeField] private List<CompletePotionViewSlot> _slots;

    private void Start()
    {
        _openButton.onClick.AddListener(FillCurrentBottles);
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
}
