using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitializator : MonoBehaviour
{   
    public static event Action OnInitComplete;

    [SerializeField] private CameraMovement _startCameraPos;

    [Header("Настройки для запуска")]
    [SerializeField] private bool _directLoad;
    [SerializeField] private LevelPreset _levelPresetDirect;
    [SerializeField] private BackgroundLoader _backGroundLoader;
    [SerializeField] private StartDialogViewer _startDialogViewer;

    [Header("Системы")]
    [SerializeField] private Inventory _inventory;
    [SerializeField] private BottleStorage _bottleStorage;
    [SerializeField] private PotionTaskSystem _potionTaskSystem;
    [SerializeField] private TradeSystem _tradeSystem;
    [SerializeField] private VisitorController _visitorController;
    [SerializeField] private MixingSystemv3 _mixingSystem;
    
    [Header("Подсветка и UI")]
    [SerializeField] private MoneyView _moneyView;
    [SerializeField] private BrightObject _brightObjectSystem;
    [SerializeField] private UIController _UIController;    
    [SerializeField] private GameStateController _gameStateController;
    [SerializeField] private CompleteLevel _levelCompletePanel;

    private Money _money;
    private LevelPreset _levelPreset;
    private List<CounterTask> _tasksChance;

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
        //if (_levelPreset.levelTaskText.Length != 0)
        //{
        //    _startDialogViewer.InitDialog(_levelPreset.levelTaskText);
        //}

        _startDialogViewer.DisableViewer();

        InitLevelSettings();
        InitInventory();
        InitSystems();
        
    }  

    private void InitLevelSettings()
    {
        _money = new Money(_moneyView,_levelPreset.startMoney, _levelPreset.minRangeMoney);    

        _backGroundLoader.SetBackGround(_levelPreset.backgroundSprite);
        _levelCompletePanel.Init(_money);
    }

    private void InitInventory()
    {
        _inventory.InitInventory();
        _inventory.FillCommonIngredients(_levelPreset.addCommonResourceCount);
        _bottleStorage.InitBottleStorage(_levelPreset.startBottleCount);
    }

    private void InitSystems()
    {
        SetChances();
        _potionTaskSystem.Init(_tasksChance, _money);
        
        _visitorController.InitVisitorController(_potionTaskSystem,_levelPreset.visitorCount);
        _gameStateController.Init(_mixingSystem);
        _tradeSystem.Init(_visitorController,_money);

        OnInitComplete?.Invoke();
    }

    private void SetChances()
    {
        _tasksChance = new List<CounterTask>();

        _tasksChance.Add(new CounterTask(1, _levelPreset.chance1Label));
        _tasksChance.Add(new CounterTask(2, _levelPreset.chance2Label));
        _tasksChance.Add(new CounterTask(3, _levelPreset.chance3Label));
    }
}
