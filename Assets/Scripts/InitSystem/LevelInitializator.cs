using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitializator : MonoBehaviour
{   
    public static event Action OnInitComplete;

    public const int stockAmount = 5;

    [Header("��������� ��� �������")]
    [SerializeField] private bool _directLoad;
    [SerializeField] private LevelPreset _levelPresetDirect;

    [Header("���������� �������")]
    [SerializeField] private Shop _shopSystem;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private BottleStorage _bottleStorage;
    [SerializeField] private BackgroundLoader _backGroundLoader;
    [SerializeField] private StartDialogViewer _startDialogViewer;
    [SerializeField] private UniversalGlobalTask _universalGlobalTask;
    [SerializeField] private GameManager _gameManager;
    
    [Header("���������")]
    [SerializeField] private TutorialManager _tutorialManager;
    [SerializeField] private PotionTaskSystem _potionTaskSystem;
    [SerializeField] private ContrabandPotionSystem _contrabandPotionSystem;

    [Header("��������")]
    [SerializeField] private RentCalculator _rentShop;
    [SerializeField] private GuildSystem _guildSystem;
    [SerializeField] private GameTimer _gameTimer;

    [Header("��������������� �������")]
    [SerializeField] private CameraMovement _startCameraPos;
    [SerializeField] private ShopController _shopController;
    [SerializeField] private VisitorController _visitorController;
    [SerializeField] private BrightObject _brightObjectSystem;
    [SerializeField] private UIController _UIController;
    [SerializeField] private MoneyView _moneyView;
    [SerializeField] private GameStateController _gameStateController;

    //private GlobalTask _currentGlobalTask;
    private Money _money;
    private LevelPreset _levelPreset;
    private LevelMoneyTask _levelMoneyTask;

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
        _levelMoneyTask = new LevelMoneyTask(_levelPreset.MoneyGoal,_levelPreset.minRangeMoney);
        _money = new Money(_moneyView,_levelPreset.startMoney, _levelPreset.minRangeMoney);    

        _backGroundLoader.SetBackGround(_levelPreset.backgroundSprite);

        TutorialCheck();

    }

    private void InitInventory()
    {
        _inventory.InitInventory();
        _inventory.FillCommonIngredients(_levelPreset.addCommonResourceCount);
        _bottleStorage.InitBottleStorage(_levelPreset.startBottleCount);
    }

    private void InitSystems()
    {
        _guildSystem.InitGuildSystem();

        if (_universalGlobalTask != null)
        {
            _universalGlobalTask.Init(_levelMoneyTask,_inventory, _gameManager,_levelPreset, _levelPreset.goalText);
        }

        //_potionTaskSystem.SetTutorialMode(_levelPreset.isTutorial);
        _potionTaskSystem.Init(_money,_levelPreset.sizer, _levelPreset.countPotionInSizer);

        _universalGlobalTask.CheckContrabandLevel();
        
        _visitorController.InitVisitorController(_potionTaskSystem, _levelPreset.visitorTime, _levelPreset.contrabandVisitorTimer,_levelPreset.visitorCount);

        //_shopSystem.InitShop(_money);
        //_shopSystem.HideForTutorial(_levelPreset.isTutorial);
        
        _gameTimer.InitTimer(_levelPreset.levelTimeInSeconds, _levelPreset.timerIsActive);
        //_rentShop.InitRentSystem(_levelPreset.rent, _levelPreset.secondsForRent, _levelPreset.rentActive);
        _gameStateController.Init();

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
