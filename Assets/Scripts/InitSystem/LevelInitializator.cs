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
    [SerializeField] private ShopController _shopController;
    [SerializeField] private Money _money;
    [SerializeField] private VisitorController _visitorController;
    [SerializeField] private BrightObject _brightObjectSystem;

    [SerializeField] private LevelPreset _levelPreset;
    //private LevelPreset _levelPreset;

    private LevelTask _levelTask;
    //private bool _tutorial;

    public LevelTask LevelTask => _levelTask;

    private void Awake()
    {
    //    Application.targetFrameRate = 75;

    //    if (LevelPresetLoader.instance.LevelPreset != null)
    //    {
    //        _levelPreset = LevelPresetLoader.instance.LevelPreset;
    //    }
    }

    private void Start()
    {        
        LevelTaskInit();
        _taskSystem.InitPotionSizer();

        _money.SetStartMoney(_levelPreset.startMoney);
        _startCameraPos.SetStartPosition(_levelPreset.startWindow);
        _taskSystem.SetPotionSizer(_levelPreset.rareTask);

        switch (_levelPreset.levelNumber)
        {
            case LevelNumber.EndlessLevel:
                _eventDialogCounter.enabled = false;
                _dialogManager.enabled = false;
                StartCoroutine(_tutorialSystem.StartTutorialDelay(false));
                _taskSystem.TutorialMode(false);

                _inventory.FillClearInventory(stockAmount);
                _inventory.AddBottle(stockBottleAmount);

                _globalTaskController.SetTaskValue(100000); //!!!
                _globalTaskController.DisableTask();

                _brightObjectSystem.BrightObjects(false);
                break;

            case LevelNumber.Tutorial:

                DragController.instance.ObjectsInterractable(false);

                StartCoroutine(_tutorialSystem.StartTutorialDelay(true));

                _taskSystem.SetTutorialMode();
                _taskSystem.TutorialMode(true);

                _inventory.FillClearInventory(0);
                _inventory.AddBottle(stockBottleAmount);
                _inventory.HideRareShelf();

                _eventDialogCounter.SetEventCounterArray(_levelPreset.eventCount);
                _dialogManager.SetDialogArray(_levelPreset.dialog);
                _globalTaskController.SetTaskValue(_levelPreset.completeGoal);

                break;

            case LevelNumber.Level2:
                _eventDialogCounter.enabled = false;
                _dialogManager.enabled = false;
                StartCoroutine(_tutorialSystem.StartTutorialDelay(false));
                _taskSystem.TutorialMode(false);
                _inventory.FillClearInventory(stockAmount);
                _inventory.AddBottle(stockBottleAmount);
                _globalTaskController.SetTaskValue(_levelPreset.completeGoal); //!!!
                _brightObjectSystem.BrightObjects(false);
                break;
        }

        _visitorController.InitVisitorController();

        CheckShopController();
        
    }  

    private void LevelTaskInit()
    {
        _levelTask = new LevelTask();

        _levelTask.SetMoneyTask(_levelPreset.completeGoal);
    }

    private void CheckShopController()
    {
        if (_levelPreset.ShopController)
        {
            _shopController.gameObject.SetActive(true);
        }
        else
        {
            _shopController.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if(LevelPresetLoader.instance != null)
        LevelPresetLoader.instance.ResetPreset();
    }
}
