using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UniversalGlobalTask : MonoBehaviour
{
    public static Action OnLevelComplete;

    [SerializeField] private IngredientData[] _ingredientData;
    [SerializeField] private LevelPreset _currentLevelPreset;
    [SerializeField] private TMP_Text _taskText;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Money _money;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private PotionTaskSystem _potionTaskSystem;
    [SerializeField] private ReputationReducer _reputationReducer;
    [SerializeField] private ContrabandPotionSystem _contrabandSystem;

    private CollectableIngredient _collectable;
    private PriceSetter _priceSetter;

    private bool _inventoryComplete;
    private bool _moneyComplete;
    public void Init()
    {
        Money.OnMoneyChanged.AddListener(CheckGoalMoneyTask);
        Money.OnMoneyChanged.AddListener(CheckFailMoneyTask);
        Money.OnMoneyChanged.AddListener(CheckCompleteLevel);

        SetMultiplyReward();

        if (_currentLevelPreset.isCollectIngredient)
        {
            _collectable = new CollectableIngredient(_currentLevelPreset, _inventory);
            
            Inventory.OnItemValueChanged += _collectable.CheckInventory;
            Inventory.OnItemValueChanged += CheckCollectableTask;
            Inventory.OnItemValueChanged += CheckCompleteLevel;
        }

        if (_currentLevelPreset.isCostMultiplyActive)
        {
            _priceSetter = new PriceSetter(_currentLevelPreset.ingredientsForMultiplyCost);
            _priceSetter.ChangeCost(_ingredientData, _currentLevelPreset.multiplyCostIngredient);
        }

        if (_currentLevelPreset.isRandomResourceAdd)
        {
            if(_currentLevelPreset.countTryAddRandomResource != 0)
            {
                SetRandomResource();
            }
            else
            {
                Debug.LogWarning("Не указано количество отдаваемых ресурсв");
            }
            
        }

        _reputationReducer.InitReducer(_currentLevelPreset.reduceValue, _currentLevelPreset.timeDecreaseReputation);
    }

    private void CheckCompleteLevel()
    {
        if (_currentLevelPreset.isCollectIngredient)
        {
            if (_moneyComplete && _inventoryComplete)
            {
                _gameManager.CompleteLevel();
                OnLevelComplete?.Invoke();
                return;
            }
        }
        else
        {
            if (_moneyComplete)
            {
                _gameManager.CompleteLevel();
                OnLevelComplete?.Invoke();
            }
        }         
    }

    public void SetLevelPreset(LevelPreset levelPreset)
    {
        _currentLevelPreset = levelPreset;
        //_levelNumber = _currentLevelPreset.levelNumber;
    }

    public void SetLevelTaskText(string text)
    {
        _taskText.text = text;
    }

    public void CheckGoalMoneyTask()
    {
        if (_currentLevelPreset.MoneyGoal <= _money.CurrentMoney)
        {           
            _moneyComplete = true;
        }
    }

    public void CheckCollectableTask()
    {
        if (_collectable.InventroryComplete)
        {
            _inventoryComplete = true;
        }
    }

    public void CheckFailMoneyTask()
    {
        if (_currentLevelPreset.minRangeMoney >= _money.CurrentMoney)
        {
            _gameManager.DefeatLevel();
        }
    }

    private void SetMultiplyReward()
    {
        _potionTaskSystem.SetRewardMultiply(_currentLevelPreset._rewardMultiply);
    }

    public void CheckContrabandLevel()
    {
        if (_currentLevelPreset.isContrabandLevel)
        {
            _contrabandSystem.InitContrabandPotion(_currentLevelPreset.contrabandTimer,_currentLevelPreset.contrabadPotionChance);
            string text = "\n\nКОНТРАБАНДНОЕ ЗЕЛЬЕ - " + _contrabandSystem.ContrabandPotion.PotionName;

            _taskText.text += text;
        }       
    }

    private void SetRandomResource()
    {
        for (int i = 0; i < _currentLevelPreset.countTryAddRandomResource; i++)
        {
            if (_currentLevelPreset.addCommonResource)
            {
                _inventory.AddIngredientWithIndex(UnityEngine.Random.Range(0, 3), _currentLevelPreset.countRandomResource);
            }

            if (_currentLevelPreset.addRareResource)
            {
                _inventory.AddIngredientWithIndex(UnityEngine.Random.Range(4, 7), _currentLevelPreset.countRandomResource);
            }
        }       
    }

    private void OnDisable()
    {
        _priceSetter?.ResetPrice();
    }
}
