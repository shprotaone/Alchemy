using UnityEngine;

public class Shelf : MonoBehaviour
{
    [SerializeField] private Slot[] _slots;

    public Slot[] Slots => _slots;

    public void HideShelf()
    {
        foreach (var slot in _slots)
        {
            slot.SlotView.HideSlotView();
        }
    }
}
