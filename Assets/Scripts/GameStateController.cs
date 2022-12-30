using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private VisitorController _visitorController;
    [SerializeField] private Button _cameraMovementButton;

    public bool InventoryIsEmpty { get; private set; }

    public void Init()
    {
        MixingSystemv3.OnIngredientAdded += CheckLeftIngredients;
        _cameraMovementButton.interactable = false;
    }

    public void CheckState()
    {
        if (InventoryIsEmpty)
        {
            _cameraMovementButton.interactable = true;
            _visitorController.Activate();
        }
        else
        {
            _cameraMovementButton.interactable = false;
            _visitorController.Deactivate();
        }
    }

    public void CheckLeftIngredients()
    {
        int ingredientLeft = 0;

        foreach (var item in _inventory.Slots)
        {
            ingredientLeft += item.AmountInSlot;
        }

        if (ingredientLeft <= 1 && !InventoryIsEmpty)
        {
            InventoryIsEmpty = true;
            CheckState();
        }
    }

    private void OnDisable()
    {
        MixingSystemv3.OnIngredientAdded -= CheckLeftIngredients;
    }
}
