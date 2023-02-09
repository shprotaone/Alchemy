using UnityEngine;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private VisitorController _visitorController;
    [SerializeField] private CameraMovement _cameraMovement;
    [SerializeField] private AudioManager _audioManager;

    private MixingSystemv3 _mixingSystem;
    private LevelPreset _preset;
    public bool InventoryIsEmpty { get; private set; }

    public void Init(MixingSystemv3 mixingSystem,LevelPreset preset)
    {
        _mixingSystem = mixingSystem;
        _preset = preset;

        InventoryIsEmpty = false;
        _mixingSystem.OnBottleFilled += CheckLeftIngredients;
    }

    public void CheckState()
    {
        if (InventoryIsEmpty)
        {          
            _cameraMovement.Movement();
            _visitorController.Activate();
            _audioManager.ChangeMainMusic(_preset._mainSound);
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
