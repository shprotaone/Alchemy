using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitializator : MonoBehaviour
{
    public static Action<string[]> OnStartWindowInit;   //инициализирует стартовое диалоговое окно
    public static Action<Sprite> OnBackGroundInit;

    public static event Action OnInitComplete;

    public const int stockAmount = 5;

    [Header("Настройки для запуска")]
    [SerializeField] private bool _directLoad;
    [SerializeField] private LevelPreset _levelPresetDirect;

    [Header("Глобальные объекты")]
    [SerializeField] private Shop _shopSystem;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Money _money;
    
    [Header("Менеджеры")]
    [SerializeField] private TutorialManager _tutorialManager;
    [SerializeField] private List<GlobalTask> _levelTasks;
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

    private GlobalTask _currentGlobalTask;
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
        GetGlobalTask();

        _inventory.InitInventory();
        OnBackGroundInit?.Invoke(_levelPreset.backgroundSprite);       
              
        _guildSystem.InitGuildSystem();

        LevelInitSelector();
        _visitorController.InitVisitorController();

        CheckShopController();
        
    }  

    private void LevelInitSelector()
    {
        _money.SetStartMoney(_levelPreset.startMoney, _levelPreset.minRangeMoney);

        if(_currentGlobalTask != null)
        {
            _currentGlobalTask.Init();
            _currentGlobalTask.SetTaskValue(_levelPreset.MoneyGoal, _levelPreset.minRangeMoney);
        }

        _taskSystem.Init();
        _taskSystem.SetPotionSizer(_levelPreset.levelNumber);
            
        switch (_levelPreset.levelNumber)
        {
            case LevelNumber.EndlessLevel:

                OnStartWindowInit?.Invoke(_levelPreset.levelTaskText);
                
                _inventory.FillFullInventory(stockAmount);
                _gameTimer.InitTimer(0, false);

                break;

            case LevelNumber.Level1:

                DragController.instance.ObjectsInterractable(false);                

                _tutorialManager.Init();
                _tutorialManager.NextStep();

                _taskSystem.SetTutorialMode(true);         
                
                _shopSystem.HideForTutorial(true);
                _gameTimer.InitTimer(0, false);

                _inventory.FillCommonIngredients(_levelTasks[0].CommonResourceCount);
                _inventory.HideRareShelf();
               
                break;

            case LevelNumber.Level2:

                OnBackGroundInit?.Invoke(_levelPreset.backgroundSprite);
                OnStartWindowInit?.Invoke(_levelPreset.levelTaskText);

                _gameTimer.InitTimer(_levelPreset.levelTimeInSeconds, true);

                _rentShop.InitRentSystem(_levelPreset.rent,_levelPreset.secondsForRent);
                _inventory.FillCommonIngredients(_currentGlobalTask.CommonResourceCount);

                break;

            case LevelNumber.Level3:

                OnBackGroundInit?.Invoke(_levelPreset.backgroundSprite);
                OnStartWindowInit?.Invoke(_levelPreset.levelTaskText);

                _gameTimer.InitTimer(_levelPreset.levelTimeInSeconds, true);
                _rentShop.InitRentSystem(_levelPreset.rent, _levelPreset.secondsForRent);

                _inventory.FillCommonIngredients(_currentGlobalTask.CommonResourceCount);
                _inventory.AddIngredientWithIndex(UnityEngine.Random.Range(4, 7), 1);   //условие случайный редкий ресурс

                break;

            case LevelNumber.Level3a:

                OnBackGroundInit?.Invoke(_levelPreset.backgroundSprite);
                OnStartWindowInit?.Invoke(_levelPreset.levelTaskText);

                _inventory.FillCommonIngredients(_currentGlobalTask.CommonResourceCount);
                _rentShop.InitRentSystem(_levelPreset.rent, _levelPreset.secondsForRent);

                _taskSystem.SetPotionSizer(LevelNumber.Level3a, 2);

                if (_currentGlobalTask is GlobalTask3a task)
                    _contrabandPotionSystem.InitContrabandPotion(task.TimeToContrabandPotion);

                _gameTimer.InitTimer(_levelPreset.levelTimeInSeconds, true);

                break;

            case LevelNumber.Level4:

                OnBackGroundInit?.Invoke(_levelPreset.backgroundSprite);
                OnStartWindowInit?.Invoke(_levelPreset.levelTaskText);     

                _inventory.FillCommonIngredients(_currentGlobalTask.CommonResourceCount);
                _rentShop.InitRentSystem(_levelPreset.rent, _levelPreset.secondsForRent);

                _gameTimer.InitTimer(_levelPreset.levelTimeInSeconds, true);

                break;

            case LevelNumber.Level5:

                OnBackGroundInit?.Invoke(_levelPreset.backgroundSprite);
                OnStartWindowInit?.Invoke(_levelPreset.levelTaskText);

                _inventory.FillCommonIngredients(_currentGlobalTask.CommonResourceCount);
                _rentShop.InitRentSystem(_levelPreset.rent, _levelPreset.secondsForRent);

                _gameTimer.InitTimer(_levelPreset.levelTimeInSeconds, true);

                break;

            case LevelNumber.Level6:
                OnBackGroundInit?.Invoke(_levelPreset.backgroundSprite);
                OnStartWindowInit?.Invoke(_levelPreset.levelTaskText);

                _inventory.FillCommonIngredients(_currentGlobalTask.CommonResourceCount);
                _rentShop.InitRentSystem(_levelPreset.rent, _levelPreset.secondsForRent);
                _gameTimer.InitTimer(_levelPreset.levelTimeInSeconds, true);

                break;
        }

        OnInitComplete?.Invoke();
    }

    private void LevelTaskInit()
    {
        _levelTask = new LevelTask();

        _levelTask.SetMoneyTask(_levelPreset.MoneyGoal);
    }

    private void GetGlobalTask()
    {

        foreach (var item in _levelTasks)
        {
            if (item.LevelNumber == _levelPreset.levelNumber)
                _currentGlobalTask = item;            
        }
       
        if(_currentGlobalTask != null)
           _currentGlobalTask.gameObject.SetActive(true);
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
