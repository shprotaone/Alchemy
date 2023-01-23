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
    [SerializeField] private StartDialogViewer _startDialogViewer;

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
    [SerializeField] private Transform _guideTransform;
    [SerializeField] private LabelToSprite _labelToSprite;

    private Money _money;
    private MoneyTask _moneyTask;
    private GameProgressSaver _gameSaver;

    private List<CounterTask> _tasksChance;
    
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
        _startDialogViewer.DisableViewer();
        _audioManager.Init(_gameSaver);
        _audioManager?.MainMusicSoruce.Play();

        InitTask();

        if (_money == null)
        {
            _money = new Money(_moneyView, _currentLevelPreset.startMoney,_moneyTask.TaskMoney,_currentLevelPreset.minRangeMoney);
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
        _potionTaskSystem.Init(_tasksChance, _labelToSprite);
        
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

        _gameSaver.FirstGameComplete();
    }

    public void LoadNextLevel(LevelPreset preset)
    {
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
        _moneyView.RefreshMoneyTaskText(_money.CurrentMoney);

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
            _guideTransform.gameObject.SetActive(true);
        }      
    }

    private void OnDisable()
    {
        DOTween.KillAll();
    }
}
