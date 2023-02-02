using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitializator : MonoBehaviour
{
    public static event Action OnLevelStarted;

    [SerializeField] private CameraMovement _startCameraPos;

    [Header("��������� ��� �������")]
    [SerializeField] private bool _directLoad;
    [SerializeField] private LevelPreset _currentLevelPreset;
    [SerializeField] private BackgroundLoader _backGroundLoader;

    [Header("�������")]
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private BottleStorage _bottleStorage;
    [SerializeField] private PotionTaskSystem _potionTaskSystem;
    [SerializeField] private TradeSystem _tradeSystem;
    [SerializeField] private VisitorController _visitorController;
    [SerializeField] private MixingSystemv3 _mixingSystem;
    [SerializeField] private ClickController _clickController;
    [SerializeField] private CameraMovement _cameraMovement;
    [SerializeField] private AudioManager _audioManager;

    [Header("��������� � UI")]
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
    
    private void Start()
    {
        Application.targetFrameRate = -1;
        DOTween.SetTweensCapacity(1250,500);

        _gameSaver = new GameProgressSaver();

        _levelSelector.Init();
        _currentLevelPreset = _levelSelector.CurrentLevel;
        InitLevelSettings();
        _dayEntryController.CallNextDay((int)_levelSelector.CurrentLevel.levelNumber).OnKill(FirstStart);
    }  

    public void InitLevelSettings()
    {
        _audioManager.Init(_gameSaver);
        _audioManager?.MainMusicSoruce.Play();

        InitTask();

        if (_money == null)
        {
            _money = new Money(_moneyView ,0,_moneyTask.TaskMoney,0);
        }

        _gameManager.Init(_money,_gameSaver);
        _moneyView.InitSlider(_money.CurrentMoney, _moneyTask.TaskMoney);
        _backGroundLoader.SetBackGround(_currentLevelPreset.backgroundSprite);
        _levelCompletePanel.Init(_money, _moneyTask,_levelSelector,_audioManager);
        _cameraMovement.Init();
        _clickController.InitializeProgressBar();

        InitInventory();
        InitSystems();
    }

    private void InitInventory()
    {
        _inventory.InitInventory();
        _inventory.FillCommonIngredients(_currentLevelPreset.addCommonResourceCount);
        _bottleStorage.InitBottleStorage(_currentLevelPreset.startBottleCount,_labelToSprite);
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
        _tasksChance = new List<RandomPart>();

        _tasksChance.Add(new RandomPart(1, _currentLevelPreset.chance1Label));
        _tasksChance.Add(new RandomPart(2, _currentLevelPreset.chance2Label));
        _tasksChance.Add(new RandomPart(3, _currentLevelPreset.chance3Label));

        _labelTypeForTaskChance = new List<RandomPart>();

        _labelTypeForTaskChance.Add(new RandomPart(1, _currentLevelPreset.chanceWater));
        _labelTypeForTaskChance.Add(new RandomPart(2, _currentLevelPreset.chanceFire));
        _labelTypeForTaskChance.Add(new RandomPart(3, _currentLevelPreset.chanceStone));
    }

    public void DisableLevel()
    {
        _visitorController.Disable();
        _tradeSystem.Disable();
        _gameStateController.Disable();
        _clickController.Disable();

        _gameSaver.FirstGameComplete();
        _gameSaver.SaveAchievments();
    }

    public void LoadNextLevel(LevelPreset preset)
    {
        _moneyView.RefreshMoneyTaskText(_money.CurrentMoney);
        _moneyView.RefreshMoneyText(_money.CurrentMoney);
        _currentLevelPreset = preset;

        _levelCompletePanel.Disable();
        _cameraMovement.Movement();
        InitLevelSettings();
        _dayEntryController.CallNextDay((int)preset.levelNumber);
        OnLevelStarted?.Invoke();       
    }

    public void RestartGame()
    {
        DisableLevel();
        _money.SetMoney(0);
        _currentLevelPreset = _levelSelector.GetFirstLevel();
        _levelCompletePanel.Disable();
        _dayEntryController.CallNextDay((int)_levelSelector.CurrentLevel.levelNumber);
        _cameraMovement.Movement();
        InitLevelSettings();
        OnLevelStarted?.Invoke();

    }

    private void FirstStart()
    {
        if (_gameSaver.IsFirstGame)
        {
            _firstPlayHandler.Activate();
        }
    }

    private void OnDestroy()
    {
        DOTween.KillAll();
    }
}
