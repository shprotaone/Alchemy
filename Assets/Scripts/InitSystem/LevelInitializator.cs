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
    [SerializeField] private Inventory _inventory;
    [SerializeField] private BottleStorage _bottleStorage;
    [SerializeField] private BackgroundLoader _backGroundLoader;
    [SerializeField] private StartDialogViewer _startDialogViewer;
    [SerializeField] private UniversalGlobalTask _universalGlobalTask;
    [SerializeField] private GameManager _gameManager;
    
    [Header("Менеджеры")]
    [SerializeField] private TutorialManager _tutorialManager;
    [SerializeField] private PotionTaskSystem _potionTaskSystem;
    [SerializeField] private ContrabandPotionSystem _contrabandPotionSystem;

    [Header("Вспомогательные системы")]
    [SerializeField] private TradeSystem _tradeSystem;
    [SerializeField] private CameraMovement _startCameraPos;
    [SerializeField] private VisitorController _visitorController;
    [SerializeField] private BrightObject _brightObjectSystem;
    [SerializeField] private UIController _UIController;
    [SerializeField] private MoneyView _moneyView;
    [SerializeField] private GameStateController _gameStateController;
    [SerializeField] private MixingSystemv3 _mixingSystem;
    [SerializeField] private CompleteLevel _levelCompletePanel;

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
        _levelMoneyTask = new LevelMoneyTask(_levelPreset.MoneyGoal,_levelPreset.minRangeMoney);
        _money = new Money(_moneyView,_levelPreset.startMoney, _levelPreset.minRangeMoney);    

        _backGroundLoader.SetBackGround(_levelPreset.backgroundSprite);
        _levelCompletePanel.Init(_money);
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
        _potionTaskSystem.Init(_money);
        
        _visitorController.InitVisitorController(_potionTaskSystem, _levelPreset.visitorTime, _levelPreset.contrabandVisitorTimer,_levelPreset.visitorCount);

        _gameStateController.Init(_mixingSystem);
        _tradeSystem.Init(_visitorController,_money);

        OnInitComplete?.Invoke();
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
