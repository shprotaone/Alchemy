using UnityEngine;

public class LevelInitializator : MonoBehaviour
{
    public const int stockAmount = 5;

    [SerializeField] private bool _directLoad;

    [SerializeField] private BackgroundLoader _backgroundLoader;
    [SerializeField] private TutorialManager _tutorialManager;
    [SerializeField] private ShopSystem _shopSystem;
    [SerializeField] private CameraMovement _startCameraPos;    
    [SerializeField] private PotionTaskSystem _taskSystem;
    [SerializeField] private GlobalTaskController _globalTaskController;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private ShopController _shopController;
    [SerializeField] private Money _money;
    [SerializeField] private VisitorController _visitorController;
    [SerializeField] private BrightObject _brightObjectSystem;
    [SerializeField] private UIController _UIController;
    [SerializeField] private GameTimer _gameTimer;

    [SerializeField] private LevelPreset _levelPresetDirect;
    
    private LevelPreset _levelPreset;

    private LevelTask _levelTask;

    public LevelTask LevelTask => _levelTask;

    private void Awake()
    {
        Application.targetFrameRate = 75;

        if (_directLoad)
        {
            _levelPreset = _levelPresetDirect;
        }
        else
        {
            if (LevelPresetLoader.instance.LevelPreset != null)
            {
                _levelPreset = LevelPresetLoader.instance.LevelPreset;
            }
        }
    }

    private void Start()
    {        
        LevelTaskInit();
        _taskSystem.InitPotionSizer();
        _inventory.InitInventory();
        _backgroundLoader.InitBackground(_levelPreset.backgroundSprite);

        _money.SetStartMoney(_levelPreset.startMoney);
        _startCameraPos.SetStartPosition(_levelPreset.startWindow);
        _taskSystem.SetPotionSizer(_levelPreset.rareTask);
              
        LevelInitSelector();

        _visitorController.InitVisitorController();

        CheckShopController();
        
    }  

    private void LevelInitSelector()
    {
        switch (_levelPreset.levelNumber)
        {
            case LevelNumber.EndlessLevel:

                _tutorialManager.gameObject.SetActive(false);

                _globalTaskController.CallStartGlobalTaskViewer(_levelPreset.levelTaskText);

                _inventory.FillFullInventory(stockAmount);

                _globalTaskController.SetTaskValue(100000); //!!!
                _globalTaskController.DisableTask();

                _brightObjectSystem.BrightObjects(false);

                break;

            case LevelNumber.Tutorial:

                DragController.instance.ObjectsInterractable(false);
                _tutorialManager.Init();
                _tutorialManager.NextStep();

                _taskSystem.SetTutorialMode();
                _taskSystem.TutorialMode(true);

                _shopSystem.HideForTutorial(true);
                _gameTimer.InitTimer(1, false);

                _inventory.FillFullInventory(0);
                _inventory.HideRareShelf();

                _globalTaskController.SetTaskValue(_levelPreset.completeGoal);

                break;

            case LevelNumber.Level2:

                _globalTaskController.CallStartGlobalTaskViewer(_levelPreset.levelTaskText);

                _tutorialManager.gameObject.SetActive(false);

                _taskSystem.TutorialMode(false);
                _gameTimer.InitTimer(300, true);

                _inventory.FillCommonIngredients(stockAmount);

                _globalTaskController.SetTaskValue(_levelPreset.completeGoal); //!!!

                _brightObjectSystem.BrightObjects(false);

                break;
        }
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
            _shopController.Plate.gameObject.SetActive(true);
        }
        else
        {
            _shopController.Plate.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if(LevelPresetLoader.instance != null)
        LevelPresetLoader.instance.ResetPreset();
    }
}
