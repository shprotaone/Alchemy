using System;
using UnityEngine;

public class LevelInitializator : MonoBehaviour
{
    public static Action<string[]> OnStartWindowInit;   //инициализирует стартовое диалоговое окно
    public static Action<Sprite> OnBackGroundInit;

    public const int stockAmount = 5;

    [SerializeField] private bool _directLoad;

    //[SerializeField] private BackgroundLoader _backgroundLoader;
    [SerializeField] private TutorialManager _tutorialManager;
    [SerializeField] private ShopSystem _shopSystem;
    [SerializeField] private CameraMovement _startCameraPos;    
    [SerializeField] private PotionTaskSystem _taskSystem;
    [SerializeField] private GlobalTask1 _globalTaskController;
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

                _money.SetStartMoney(_levelPreset.startMoney, _levelPreset.minRangeMoney);
                _tutorialManager.gameObject.SetActive(false);

                _taskSystem.InitPotionSizer(LevelNumber.EndlessLevel);
                _taskSystem.SetTutorialMode(false);

                OnStartWindowInit?.Invoke(_levelPreset.levelTaskText);

                _inventory.FillFullInventory(stockAmount);

                _globalTaskController.SetTaskValue(_levelPreset.MoneyGoal,_levelPreset.minRangeMoney); //!!!
                _globalTaskController.DisableTask();

                _brightObjectSystem.BrightObjects(false);

                break;

            case LevelNumber.Level1:

                DragController.instance.ObjectsInterractable(false);

                _money.SetStartMoney(_levelPreset.startMoney, _levelPreset.minRangeMoney);

                _tutorialManager.Init();
                _tutorialManager.NextStep();

                _taskSystem.SetTutorialMode(true);
                _taskSystem.InitPotionSizer(LevelNumber.Level1);              
                
                _shopSystem.HideForTutorial(true);
                _gameTimer.InitTimer(0, false);

                _inventory.FillFullInventory(0);
                _inventory.HideRareShelf();

                _globalTaskController.SetTaskValue(_levelPreset.MoneyGoal,0);

                break;

            case LevelNumber.Level2:

                OnBackGroundInit?.Invoke(_levelPreset.backgroundSprite);
                OnStartWindowInit?.Invoke(_levelPreset.levelTaskText);

                _money.SetStartMoney(_levelPreset.startMoney, _levelPreset.minRangeMoney);
                _tutorialManager.gameObject.SetActive(false);

                _taskSystem.InitPotionSizer(LevelNumber.Level2);
                _taskSystem.SetTutorialMode(false);

                _gameTimer.InitTimer(_levelPreset.levelTimeInSeconds, true);

                _rentShop.InitRentSystem(_levelPreset.rent,_levelPreset.secondsForRent);
                _inventory.FillCommonIngredients(stockAmount);

                _globalTaskController.SetTaskValue(_levelPreset.MoneyGoal, _levelPreset.minRangeMoney);

                _brightObjectSystem.BrightObjects(false);

                break;
            case LevelNumber.Level3:

                OnBackGroundInit?.Invoke(_levelPreset.backgroundSprite);
                OnStartWindowInit?.Invoke(_levelPreset.levelTaskText);

                break;
        }
    }

    private void LevelTaskInit()
    {
        _levelTask = new LevelTask();

        _levelTask.SetMoneyTask(_levelPreset.MoneyGoal);
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
