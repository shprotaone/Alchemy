using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitializator : MonoBehaviour
{   
    public static event Action OnInitComplete;
    public static event Action OnNewGameStarted;

    [SerializeField] private CameraMovement _startCameraPos;

    [Header("��������� ��� �������")]
    [SerializeField] private bool _directLoad;
    [SerializeField] private LevelPreset _levelPresetDirect;
    [SerializeField] private LevelPreset _currentLevelPreset;

    [SerializeField] private BackgroundLoader _backGroundLoader;
    [SerializeField] private StartDialogViewer _startDialogViewer;

    [Header("�������")]
    [SerializeField] private Inventory _inventory;
    [SerializeField] private BottleStorage _bottleStorage;
    [SerializeField] private PotionTaskSystem _potionTaskSystem;
    [SerializeField] private TradeSystem _tradeSystem;
    [SerializeField] private VisitorController _visitorController;
    [SerializeField] private MixingSystemv3 _mixingSystem;
    [SerializeField] private CameraMovement _cameraMovement;

    [Header("��������� � UI")]
    [SerializeField] private MoneyView _moneyView;
    [SerializeField] private MoneyTaskView _moneyTaskView;
    [SerializeField] private BrightObject _brightObjectSystem;
    [SerializeField] private UIController _UIController;    
    [SerializeField] private GameStateController _gameStateController;
    [SerializeField] private CompleteLevel _levelCompletePanel;

    private Money _money;
    private MoneyTask _moneyTask;
    
    private List<CounterTask> _tasksChance;

    private void Awake()
    {
        Application.targetFrameRate = 75;

        if (_directLoad)
        {
            _currentLevelPreset = _levelPresetDirect;
        }
        else
        {
            if (LevelPresetLoader.instance.LevelPreset != null)
            {
                _currentLevelPreset = LevelPresetLoader.instance.LevelPreset;
            }
        }
    }

    private void Start()
    {
        InitLevelSettings();      
    }  

    public void InitLevelSettings()
    {
        _startDialogViewer.DisableViewer();

        InitTask();
        if (_money == null)
        {
            _money = new Money(_moneyView, _currentLevelPreset.startMoney, _currentLevelPreset.minRangeMoney);
        }

        _backGroundLoader.SetBackGround(_currentLevelPreset.backgroundSprite);
        _levelCompletePanel.Init(_money,_moneyTask);
        _cameraMovement.Init();

        InitInventory();
        InitSystems();
    }

    private void InitInventory()
    {
        _inventory.InitInventory();
        _inventory.FillCommonIngredients(_currentLevelPreset.addCommonResourceCount);
        _bottleStorage.InitBottleStorage(_currentLevelPreset.startBottleCount);
    }

    private void InitSystems()
    {
        SetChances();
        _potionTaskSystem.Init(_tasksChance, _money);
        
        _visitorController.InitVisitorController(_potionTaskSystem,_currentLevelPreset.visitorCount);
        _gameStateController.Init(_mixingSystem);
        _tradeSystem.Init(_visitorController,_money);

        OnInitComplete?.Invoke();
    }

    private void InitTask()
    {
        if (_moneyTask == null)
        {
            _moneyTask = new MoneyTask(_levelPresetDirect.moneyTaskComplete);
        }
        else
        {
            _moneyTask.IncreaseTask(_money.CurrentMoney, _currentLevelPreset.moneyTaskComplete);
        }
        _moneyTaskView.SetTaskText(_moneyTask.TaskMoney);
    }

    private void SetChances()
    {
        _tasksChance = new List<CounterTask>();

        _tasksChance.Add(new CounterTask(1, _currentLevelPreset.chance1Label));
        _tasksChance.Add(new CounterTask(2, _currentLevelPreset.chance2Label));
        _tasksChance.Add(new CounterTask(3, _currentLevelPreset.chance3Label));
    }

    public void SetPreset(LevelPreset preset)
    {
        _visitorController.DisableVisitor();
        _currentLevelPreset = preset;
        _levelCompletePanel.Disable();
        _cameraMovement.Movement();
        InitLevelSettings();
        OnNewGameStarted?.Invoke();
        
    }
}
