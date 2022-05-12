using System;
using UnityEngine;

[Serializable]
public class Shelf : MonoBehaviour
{
    [SerializeField] private GameObject _dragableParent;
    [SerializeField] private IngredientData[] _ingredients;

    private Slot[] _slots;

    public void Init()
    {
        _slots = GetComponentsInChildren<Slot>();
        FillShelf();
    }

    private void FillShelf()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i].FillSlot(_ingredients[i]);
        }
    }
}
