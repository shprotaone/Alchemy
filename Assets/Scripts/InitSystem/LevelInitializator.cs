using System;
using UnityEngine;

public class LevelInitializator : MonoBehaviour
{
    public static Action<string[]> OnStartWindowInit;   //инициализирует стартовое диалоговое окно
    public static Action<Sprite> OnBackGroundInit;

    public const int stockAmount = 5;

    [SerializeField] private bool _directLoad;

    [SerializeField] private GlobalTask[] _levelTasks;
    [SerializeField] private TutorialManager _tutorialManager;
    [SerializeField] private ShopSystem _shopSystem;
    [SerializeField] private CameraMovement _startCameraPos;    
    [SerializeField] private PotionTaskSystem _taskSystem;
    [SerializeField] private Inventory _inventory;

    [SerializeField] private ShopController _shopController;
    [SerializeField] private RentShop _rentShop;

    [SerializeField] private Money _money;
    [SerializeField] private VisitorController _visitorController;
    [SerializeField] private GuildSystem _guildSystem;
    [SerializeField] private BrightObject _brightObjectSystem;
    [SerializeField] private UIController _UIController;
    [SerializeField] private GameTimer _gameTimer;

    [SerializeField] private LevelPreset _levelPresetDirect;

    private GlobalTask _currentGlobalTask;
    private LevelPreset _levelPreset;
    private LevelTask _levelTask;


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
        GetGlobalTask();

        _inventory.InitInventory();
        OnBackGroundInit?.Invoke(_levelPreset.backgroundSprite);       
              
        _guildSystem.InitGuildSystem();

        LevelInitSelector();
        _visitorController.InitVisitorController();

        CheckShopController();
        
    }  

    private void LevelInitSelector()
    {
        switch (_levelPreset.levelNumber)
        {
            case LevelNumber.EndlessLevel:

                OnStartWindowInit?.Invoke(_levelPreset.levelTaskText);

                _money.SetStartMoney(_levelPreset.startMoney, _levelPreset.minRangeMoney);
                _tutorialManager.gameObject.SetActive(false);

                _taskSystem.InitPotionSizer(LevelNumber.EndlessLevel);
                _taskSystem.SetTutorialMode(false);
                
                _inventory.FillFullInventory(stockAmount);

                _brightObjectSystem.BrightObjects(false);

                break;

            case LevelNumber.Level1:

                DragController.instance.ObjectsInterractable(false);

                _currentGlobalTask.Init();
                _currentGlobalTask.SetTaskValue(_levelPreset.MoneyGoal, 0);

                _money.SetStartMoney(_levelPreset.startMoney, _levelPreset.minRangeMoney);

                _tutorialManager.Init();
                _tutorialManager.NextStep();

                _taskSystem.SetTutorialMode(true);
                _taskSystem.InitPotionSizer(LevelNumber.Level1);              
                
                _shopSystem.HideForTutorial(true);
                _gameTimer.InitTimer(0, false);

                _inventory.FillFullInventory(0);
                _inventory.HideRareShelf();
               
                break;

            case LevelNumber.Level2:

                OnBackGroundInit?.Invoke(_levelPreset.backgroundSprite);
                OnStartWindowInit?.Invoke(_levelPreset.levelTaskText);

                _currentGlobalTask.Init();
                _currentGlobalTask.SetTaskValue(_levelPreset.MoneyGoal, _levelPreset.minRangeMoney);

                _money.SetStartMoney(_levelPreset.startMoney, _levelPreset.minRangeMoney);
                _tutorialManager.gameObject.SetActive(false);

                _taskSystem.InitPotionSizer(LevelNumber.Level2);
                _taskSystem.SetTutorialMode(false);

                _gameTimer.InitTimer(_levelPreset.levelTimeInSeconds, true);

                _rentShop.InitRentSystem(_levelPreset.rent,_levelPreset.secondsForRent);
                _inventory.FillCommonIngredients(5);
                
                
                _brightObjectSystem.BrightObjects(false);

                break;

            case LevelNumber.Level3:

                OnBackGroundInit?.Invoke(_levelPreset.backgroundSprite);
                OnStartWindowInit?.Invoke(_levelPreset.levelTaskText);

                _inventory.FillCommonIngredients(3);
                _inventory.AddIngredientWithIndex(UnityEngine.Random.Range(4, 7), 1);   //условие случайный редкий ресурс

                _currentGlobalTask.Init();
                _currentGlobalTask.SetTaskValue(_levelPreset.MoneyGoal, _levelPreset.minRangeMoney);

                _money.SetStartMoney(_levelPreset.startMoney, _levelPreset.minRangeMoney);
                _rentShop.InitRentSystem(_levelPreset.rent, _levelPreset.secondsForRent);

                _taskSystem.InitPotionSizer(LevelNumber.Level2);
                _taskSystem.SetTutorialMode(false);

                _gameTimer.InitTimer(_levelPreset.levelTimeInSeconds, true);

                _brightObjectSystem.BrightObjects(false);
                break;

            case LevelNumber.Level3a:

                OnBackGroundInit?.Invoke(_levelPreset.backgroundSprite);
                OnStartWindowInit?.Invoke(_levelPreset.levelTaskText);

                _inventory.FillCommonIngredients(10);

                _currentGlobalTask.Init();
                _currentGlobalTask.SetTaskValue(_levelPreset.MoneyGoal, _levelPreset.minRangeMoney);

                _money.SetStartMoney(_levelPreset.startMoney, _levelPreset.minRangeMoney);
                _rentShop.InitRentSystem(_levelPreset.rent, _levelPreset.secondsForRent);

                _taskSystem.InitPotionSizer(LevelNumber.Level2);
                _taskSystem.SetTutorialMode(false);
                _gameTimer.InitTimer(_levelPreset.levelTimeInSeconds, true);

                _brightObjectSystem.BrightObjects(false);

                break;
        }
    }

    private void LevelTaskInit()
    {
        _levelTask = new LevelTask();

        _levelTask.SetMoneyTask(_levelPreset.MoneyGoal);
    }

    private void GetGlobalTask()
    {
        switch (_levelPreset.levelNumber)
        {
            case LevelNumber.Level1:
                _currentGlobalTask = _levelTasks[0];
                break;
            case LevelNumber.Level2:
                _currentGlobalTask = _levelTasks[1];
                break;
            case LevelNumber.Level3:
                _currentGlobalTask = _levelTasks[2];
                break;
            case LevelNumber.Level3a:
                _currentGlobalTask = _levelTasks[3];
                break;
            default:
                break;
        }
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
}
