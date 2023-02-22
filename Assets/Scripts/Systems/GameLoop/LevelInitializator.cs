using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class LevelInitializator : MonoBehaviour
{
    public static event Action OnLevelEnded;
    public static event Action OnLevelStarted;
    public static event Action OnLoopRestart;

    [SerializeField] private CameraMovement _startCameraPos;

    [Header("Настройки для запуска")]
    [SerializeField] private bool _directLoad;
    [SerializeField] private LevelPreset _currentLevelPreset;
    [SerializeField] private BackgroundLoader _backGroundLoader;

    [Header("Системы")]
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private BottleStorage _bottleStorage;
    [SerializeField] private PotionTaskSystem _potionTaskSystem;
    [SerializeField] private TradeSystem _tradeSystem;
    [SerializeField] private VisitorController _visitorController;
    [SerializeField] private MixingSystem _mixingSystem;
    [SerializeField] private ClickController _clickController;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private AchievementsProgressSaver _achievementsProgressSaver;

    [Header("Подсветка и UI")]
    [SerializeField] private MoneyView _moneyView;
    [SerializeField] private BrightObject _brightObjectSystem;
    [SerializeField] private UIController _UIController;
    [SerializeField] private GameStateController _gameStateController;
    [SerializeField] private LevelSelector _levelSelector;
    [SerializeField] private CompleteLevel _levelCompletePanel;
    [SerializeField] private DayEntryController _dayEntryController;
    [SerializeField] private FirstPlayObserver _firstPlayHandler;
    [SerializeField] private LabelToSprite _labelToSprite;
    [SerializeField] private DraggableObjectController _dragController;

    private Money _money;
    private MoneyTask _moneyTask;
    private GameProgressSaver _gameSaver;

    private List<RandomPart> _tasksChance;
    private List<RandomPart> _labelTypeForTaskChance;

    private void OnEnable() => YandexGame.GetDataEvent += _gameSaver.LoadRecord;

    private void OnDisable()
    {
       YandexGame.GetDataEvent -= _gameSaver.LoadRecord;
       DOTween.KillAll();
    } 

    private void Awake()
    {
        Application.targetFrameRate = -1;
        DOTween.SetTweensCapacity(1250, 500);

        _gameSaver = new GameProgressSaver();
        _achievementsProgressSaver.GameProgressSaverHandler(_gameSaver);

    }
    private void Start()
    {
        _levelSelector.Init();
        _currentLevelPreset = _levelSelector.CurrentLevel;
        InitLevelSettings();
        _dayEntryController.CallNextDay((int)_levelSelector.CurrentLevel.levelNumber).OnKill(FirstStart);
    }  

    public void InitLevelSettings()
    {
        _audioManager.Init(_gameSaver);

        InitTask();

        if (_money == null)
        {
            _money = new Money(_moneyView ,0,_moneyTask.TaskMoney,0);
        }

        _gameManager.Init(_money,_gameSaver);
        _moneyView.InitSlider(_money.CurrentMoney, _moneyTask.TaskMoney);
        _backGroundLoader.SetBackGround(_currentLevelPreset.backgroundSprite);
        _levelCompletePanel.Init(_money, _moneyTask,_levelSelector,_audioManager);
        _clickController.InitializeProgressBar();

        InitInventory();
        InitSystems();
    }

    private void InitInventory()
    {
        _inventory.InitInventory();
        _inventory.FillCommonIngredients(_currentLevelPreset.addCommonResourceCount);
        _bottleStorage.InitBottleStorage(_labelToSprite);
    }

    private void InitSystems()
    {
        SetChances();
        _potionTaskSystem.Init(_tasksChance, _labelTypeForTaskChance, _labelToSprite,_currentLevelPreset.withEvent);
        
        _visitorController.InitVisitorController(_potionTaskSystem,_currentLevelPreset.visitorCount,_audioManager);
        _gameStateController.Init(_mixingSystem,_currentLevelPreset);
        _tradeSystem.Init(_visitorController,_money,_audioManager);
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
        _tasksChance = new List<RandomPart>
        {
            new RandomPart(1, _currentLevelPreset.chance1Label),
            new RandomPart(2, _currentLevelPreset.chance2Label),
            new RandomPart(3, _currentLevelPreset.chance3Label)
        };

        _labelTypeForTaskChance = new List<RandomPart>
        {
            new RandomPart(1, _currentLevelPreset.chanceWater),
            new RandomPart(2, _currentLevelPreset.chanceFire),
            new RandomPart(3, _currentLevelPreset.chanceStone)
        };
    }

    public void DisableLevel()
    {
        _visitorController.Disable();
        _tradeSystem.Disable();
        _gameStateController.Disable();
        _clickController.Disable();

        _gameSaver.FirstGameComplete();
        OnLevelEnded?.Invoke();
    }

    public void LoadNextLevel(LevelPreset preset)
    {
        _moneyView.RefreshMoneyTaskText(_money.CurrentMoney);
        _moneyView.RefreshMoneyView(_money.CurrentMoney);
        _currentLevelPreset = preset;

        _levelCompletePanel.Disable();
        InitLevelSettings();
        _dayEntryController.CallNextDay((int)preset.levelNumber);
        OnLevelStarted?.Invoke();       
    }

    public void RestartGame()
    {
        DisableLevel();
        _money.ResetMoneyValue(0);
        _currentLevelPreset = _levelSelector.GetFirstLevel();
        _levelCompletePanel.Disable();
        _dayEntryController.CallNextDay((int)_levelSelector.CurrentLevel.levelNumber);
        InitLevelSettings();

        OnLoopRestart?.Invoke();
        OnLevelStarted?.Invoke();

    }

    private void FirstStart()
    {
        if (_gameSaver.IsFirstGame)
        {
            _firstPlayHandler.Activate();
        }
    }
}
