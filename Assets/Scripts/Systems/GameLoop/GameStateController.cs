using UnityEngine;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private VisitorController _visitorController;
    [SerializeField] private CameraMovement _cameraMovement;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private Step4 _step4;

    private MixingSystem _mixingSystem;
    private LevelPreset _preset;
    public bool InventoryIsEmpty { get; private set; }

    public void Init(MixingSystem mixingSystem,LevelPreset preset)
    {
        _mixingSystem = mixingSystem;
        _preset = preset;
        _cameraMovement.Init();

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
            if (_step4!= null) _step4.Next();
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
