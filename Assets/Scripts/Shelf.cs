using UnityEngine;

public class Shelf : MonoBehaviour
{
    [SerializeField] private Slot[] _slots;

    public Slot[] Slots => _slots;

    public void HideShelf()
    {
        foreach (var slot in _slots)
        {
            slot.SlotImage.color = new Color(0, 0, 0, 0);
            slot.AmountText.color = new Color(0, 0, 0, 0);
        }
    }
}
