using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitializator : MonoBehaviour
{   
    public static event Action OnInitComplete;

    public const int stockAmount = 5;

    [Header("Настройки для запуска")]
    [SerializeField] private bool _directLoad;
    [SerializeField] private LevelPreset _levelPresetDirect;

    [Header("Глобальные объекты")]
    [SerializeField] private Shop _shopSystem;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Money _money;
    [SerializeField] private BottleStorage _bottleStorage;
    [SerializeField] private BackgroundLoader _backGroundLoader;
    [SerializeField] private StartDialogViewer _startDialogViewer;
    [SerializeField] private UniversalGlobalTask _universalGlobalTask;
    
    [Header("Менеджеры")]
    [SerializeField] private TutorialManager _tutorialManager;
    [SerializeField] private PotionTaskSystem _taskSystem;
    [SerializeField] private ContrabandPotionSystem _contrabandPotionSystem;

    [Header("Механики")]
    [SerializeField] private RentCalculator _rentShop;
    [SerializeField] private GuildSystem _guildSystem;
    [SerializeField] private GameTimer _gameTimer;

    [Header("Вспомогательные системы")]
    [SerializeField] private CameraMovement _startCameraPos;
    [SerializeField] private ShopController _shopController;
    [SerializeField] private VisitorController _visitorController;
    [SerializeField] private BrightObject _brightObjectSystem;
    [SerializeField] private UIController _UIController;

    //private GlobalTask _currentGlobalTask;
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

        _universalGlobalTask.SetLevelPreset(_levelPreset);
    }

    private void Start()
    {               
        if(_levelPreset.levelTaskText.Length != 0)
        {
            _startDialogViewer.InitDialog(_levelPreset.levelTaskText);
        }

        InitLevelSettings();
        InitInventory();
        InitSystems();
        
    }  

    private void InitLevelSettings()
    {
        _levelTask = new LevelTask();
        _levelTask.SetMoneyTask(_levelPreset.MoneyGoal);

        _money.SetStartMoney(_levelPreset.startMoney, _levelPreset.minRangeMoney);

        _backGroundLoader.SetBackGround(_levelPreset.backgroundSprite);

        TutorialCheck();

    }

    private void InitInventory()
    {
        _inventory.FillCommonIngredients(_levelPreset.addCommonResourceCount);
        _bottleStorage.InitBottleStorage(_levelPreset.startBottleCount);
    }

    private void InitSystems()
    {
        _guildSystem.InitGuildSystem();

        if (_universalGlobalTask != null)
        {
            _universalGlobalTask.Init();
            _universalGlobalTask.SetLevelTaskText(_levelPreset.goalText);
        }

        _taskSystem.Init();

        _taskSystem.SetPotionSizer(_levelPreset.sizer, _levelPreset.countPotionInSizer);

        _universalGlobalTask.CheckContrabandLevel();
        
        _visitorController.InitVisitorController(_levelPreset.visitorTime, _levelPreset.contrabandVisitorTimer);

        _shopSystem.HideForTutorial(_levelPreset.isTutorial);
        _taskSystem.SetTutorialMode(_levelPreset.isTutorial);
        _gameTimer.InitTimer(_levelPreset.levelTimeInSeconds, _levelPreset.timerIsActive);
        _rentShop.InitRentSystem(_levelPreset.rent, _levelPreset.secondsForRent);

        CheckShopController();

        OnInitComplete?.Invoke();
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

    private void TutorialCheck()
    {
        if (_levelPreset.isTutorial)
        {
            DragController.instance.ObjectsInterractable(false);

            _tutorialManager.Init();
            _tutorialManager.NextStep();

            _inventory.HideRareShelf();
        }
    }
}
