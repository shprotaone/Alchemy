using UnityEngine;

public class LevelInitializator : MonoBehaviour
{
    public const int stockAmount = 5;
    public const int stockBottleAmount = 2;

    [SerializeField] private TutorialSystem _tutorialSystem;
    [SerializeField] private ShopSystem _shopSystem;
    [SerializeField] private EventCounter _eventDialogCounter;
    [SerializeField] private CameraMovement _startCameraPos;    
    [SerializeField] private DialogManager _dialogManager;
    [SerializeField] private TaskSystem _taskSystem;
    [SerializeField] private Inventory _inventory;
    
    [SerializeField] private LevelPreset _levelPreset;
    private LevelTask _levelTask;
    private bool _tutorial;

    public LevelTask LevelTask => _levelTask;

    private void Start()
    {
        //if (LevelPresetLoader.instance.LevelPreset != null)
        //{
        //    _levelPreset = LevelPresetLoader.instance.LevelPreset;
        //}

        LevelTaskInit();

        _tutorial = _levelPreset.tutorialLevel;
        _startCameraPos.SetStartPosition(_levelPreset.startWindow);
        _taskSystem.SetPotionSizer(_levelPreset.rareTask);

        if (_tutorial)
        {
            DragController.instance.ObjectsInterractable(false);

            StartCoroutine(_tutorialSystem.StartTutorialDelay());
            _taskSystem.TutorialMode(true);
            _eventDialogCounter.SetEventCounterArray(_levelPreset.eventCount);
            _inventory.FillInventory(0);
            _inventory.AddBottle(stockBottleAmount);
            _dialogManager.SetDialogArray(_levelPreset.dialog);
        }
        else
        {
            _eventDialogCounter.enabled = false;
            _dialogManager.enabled = false;
            _tutorialSystem.enabled = false;
            _inventory.FillInventory(stockAmount);
            _inventory.AddBottle(stockBottleAmount);
        }                  
    }  

    private void LevelTaskInit()
    {
        _levelTask = new LevelTask();

        _levelTask.SetMoneyTask(_levelPreset.completeGoal);
    }
}
