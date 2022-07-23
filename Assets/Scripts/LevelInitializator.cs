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
    [SerializeField] private PotionTaskSystem _taskSystem;
    [SerializeField] private GlobalTaskController _globalTaskController;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Money _money;

    private LevelPreset _levelPreset;
    private LevelTask _levelTask;
    private bool _tutorial;

    public LevelTask LevelTask => _levelTask;

    private void Awake()
    {
        if (LevelPresetLoader.instance.LevelPreset != null)
        {
            _levelPreset = LevelPresetLoader.instance.LevelPreset;
        }
    }

    private void Start()
    {        
        LevelTaskInit();

        _tutorial = _levelPreset.tutorialLevel;
        _money.SetStartMoney(_levelPreset.startMoney);
        _startCameraPos.SetStartPosition(_levelPreset.startWindow);
        _taskSystem.SetPotionSizer(_levelPreset.rareTask);

        if (_tutorial)
        {
            DragController.instance.ObjectsInterractable(false);

            StartCoroutine(_tutorialSystem.StartTutorialDelay(true));
            _taskSystem.SetTutorialMode();
            _taskSystem.TutorialMode(true);            
            _inventory.FillClearInventory(0);
            _inventory.AddBottle(stockBottleAmount);
            _eventDialogCounter.SetEventCounterArray(_levelPreset.eventCount);                        
            _dialogManager.SetDialogArray(_levelPreset.dialog);
        }
        else
        {
            _eventDialogCounter.enabled = false;
            _dialogManager.enabled = false;
            StartCoroutine(_tutorialSystem.StartTutorialDelay(false));
            _taskSystem.TutorialMode(false);
            _inventory.FillClearInventory(stockAmount);
            _inventory.AddBottle(stockBottleAmount);
        }
        _globalTaskController.SetTaskValue(_levelPreset.completeGoal);
    }  

    private void LevelTaskInit()
    {
        _levelTask = new LevelTask();

        _levelTask.SetMoneyTask(_levelPreset.completeGoal);
    }

    private void TutorialLevelInit()
    {

    }

    private void OnDestroy()
    {
        LevelPresetLoader.instance.ResetPreset();
    }
}
