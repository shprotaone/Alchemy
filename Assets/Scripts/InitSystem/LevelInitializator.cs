using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitializator : MonoBehaviour
{   
    public static event Action OnLevelStarted;
    public static event Action OnLevelEnded;

    [SerializeField] private CameraMovement _startCameraPos;

    [Header("Настройки для запуска")]
    [SerializeField] private bool _directLoad;
    [SerializeField] private LevelPreset _currentLevelPreset;

    [SerializeField] private BackgroundLoader _backGroundLoader;
    [SerializeField] private StartDialogViewer _startDialogViewer;

    [Header("Системы")]
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private BottleStorage _bottleStorage;
    [SerializeField] private PotionTaskSystem _potionTaskSystem;
    [SerializeField] private TradeSystem _tradeSystem;
    [SerializeField] private VisitorController _visitorController;
    [SerializeField] private MixingSystemv3 _mixingSystem;
    [SerializeField] private ClickController _clickController;
    [SerializeField] private CameraMovement _cameraMovement;

    [Header("Подсветка и UI")]
    [SerializeField] private MoneyView _moneyView;
    [SerializeField] private BrightObject _brightObjectSystem;
    [SerializeField] private UIController _UIController;    
    [SerializeField] private GameStateController _gameStateController;
    [SerializeField] private LevelSelector _levelSelector;
    [SerializeField] private CompleteLevel _levelCompletePanel;
    [SerializeField] private DayEntryController _dayEntryController;

    private Money _money;
    private MoneyTask _moneyTask;
    private GameProgressSaver _gameSaver;
    
    private List<CounterTask> _tasksChance;

    private void Start()
    {
        Application.targetFrameRate = 75;

        _levelSelector.Init();
        _currentLevelPreset = _levelSelector.CurrentLevel;
        InitLevelSettings();
        _dayEntryController.CallNextDay(1);
    }  

    public void InitLevelSettings()
    {
        _startDialogViewer.DisableViewer();

        InitTask();

        if (_money == null)
        {
            _money = new Money(_moneyView, _currentLevelPreset.startMoney,_moneyTask.TaskMoney,_currentLevelPreset.minRangeMoney);
        }

        _gameManager.Init(_money);
        _moneyView.InitSlider(_money.CurrentMoney, _moneyTask.TaskMoney);
        _backGroundLoader.SetBackGround(_currentLevelPreset.backgroundSprite);
        _levelCompletePanel.Init(_money,_moneyTask);
        _cameraMovement.Init();
        _clickController.InitializeProgressBar();

        InitInventory();
        InitSystems();
        

        //_gameProgress.SaveCurrentLevelProgress(_money.CurrentMoney);

        Debug.Log("Денег " + _money.CurrentMoney);
        Debug.Log("Задание " + _moneyTask.TaskMoney);
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
    }

    private void InitTask()
    {
        if (_moneyTask == null)
        {
            _moneyTask = new MoneyTask(_currentLevelPreset.moneyTaskComplete);
        }
        else
        {
            _moneyTask.IncreaseTask(_money.CurrentMoney, _currentLevelPreset.moneyTaskComplete);
        }

        _moneyView.RefreshMoneyTaskText(_moneyTask.TaskMoney);
    }

    private void SetChances()
    {
        _tasksChance = new List<CounterTask>();

        _tasksChance.Add(new CounterTask(1, _currentLevelPreset.chance1Label));
        _tasksChance.Add(new CounterTask(2, _currentLevelPreset.chance2Label));
        _tasksChance.Add(new CounterTask(3, _currentLevelPreset.chance3Label));
    }

    public void DisableLevel()
    {
        _visitorController.Disable();
        _tradeSystem.Disable();
        _gameStateController.Disable();
        _clickController.Disable();

        OnLevelEnded?.Invoke();
    }

    public void LoadNextLevel(LevelPreset preset)
    {
        _moneyView.RefreshMoneyText(_money.CurrentMoney);
        _currentLevelPreset = preset;

        _levelCompletePanel.Disable();
        _cameraMovement.Movement();
        InitLevelSettings();
        _dayEntryController.CallNextDay((int)_currentLevelPreset.levelNumber);
        OnLevelStarted?.Invoke();       
    }

    public void RestartGame()
    {
        DisableLevel();
        _money.SetMoney(0);
        _moneyView.RefreshMoneyTaskText(_money.CurrentMoney);

        _currentLevelPreset = _levelSelector.GetFirstLevel();
        _levelCompletePanel.Disable();
        _dayEntryController.CallNextDay((int)_currentLevelPreset.levelNumber);
        _cameraMovement.Movement();
        InitLevelSettings();
        OnLevelStarted?.Invoke();

    }
}
