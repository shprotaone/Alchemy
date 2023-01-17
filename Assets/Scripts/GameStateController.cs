using UnityEngine;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private VisitorController _visitorController;
    [SerializeField] private Button _cameraMovementButton;
    [SerializeField] private CameraMovement _cameraMovement;

    private MixingSystemv3 _mixingSystem;
    public bool InventoryIsEmpty { get; private set; }

    public void Init(MixingSystemv3 mixingSystem)
    {
        _mixingSystem = mixingSystem;
        InventoryIsEmpty = false;
        _mixingSystem.OnBottleFilled += CheckLeftIngredients;
        _cameraMovementButton.interactable = true;
    }

    public void CheckState()
    {
        if (InventoryIsEmpty)
        {          
            _cameraMovement.Movement();
            _visitorController.Activate();
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

    public void Disable()
    {
        _mixingSystem.OnBottleFilled -= CheckLeftIngredients;
    }
}
