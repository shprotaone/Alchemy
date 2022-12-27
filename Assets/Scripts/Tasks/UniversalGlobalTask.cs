using System;
using TMPro;
using UnityEngine;

public class UniversalGlobalTask : MonoBehaviour
{
    public static event Action OnLevelComplete;

    [SerializeField] private PotionTaskSystem _potionTaskSystem;
    [SerializeField] private ContrabandPotionSystem _contrabandSystem;
    [SerializeField] private GlobalTaskView _globalTaskView;

    private GameManager _gameManager;
    private Inventory _inventory;
    private LevelPreset _currentLevelPreset;

    private LevelMoneyTask _moneyTask;
    //private CollectableIngredient _collectable;
    private PriceSetter _priceSetter;

    public void Init(LevelMoneyTask moneyTask,Inventory inventory, GameManager gameManager, LevelPreset levelPreset,string taskText)
    {
        _inventory = inventory;
        _gameManager = gameManager;
        _currentLevelPreset = levelPreset;
        _moneyTask = moneyTask;
        _globalTaskView.SetLevelTaskText(taskText);

        Inventory.OnItemValueChanged += CheckCompleteLevel;
        Money.OnChangeMoneyAction += CheckCompleteLevel;

        SetMultiplyReward();
        //SetCollectableLevel(); 
        SetMultyplyCostIngredient();
        SetRandomResource();
       
    }

    private void SetCollectableLevel()
    {
        if (_currentLevelPreset.isCollectIngredient)
        {
            //_collectable = new CollectableIngredient(_currentLevelPreset, _inventory);
        }
    }
    
    private void SetMultyplyCostIngredient()
    {
        if (_currentLevelPreset.isCostMultiplyActive)
        {
            _priceSetter = new PriceSetter(_currentLevelPreset.ingredientsForMultiplyCost);
            _priceSetter.ChangeCost(_currentLevelPreset.ingredientsForMultiplyCost,
                                    _currentLevelPreset.multiplyCostIngredient);
        }
    }
    
    private void SetMultiplyReward()
    {
        //_potionTaskSystem.SetRewardMultiply(_currentLevelPreset._rewardMultiply);
    }
    
    private void SetRandomResource()
    {
        //if (_currentLevelPreset.isRandomResourceAdd)
        //{
        //    if (_currentLevelPreset.countTryAddRandomResource != 0)
        //    {
        //        for (int i = 0; i < _currentLevelPreset.countTryAddRandomResource; i++)
        //        {
        //            if (_currentLevelPreset.addCommonResource)
        //            {
        //                _inventory.AddIngredientWithIndex(UnityEngine.Random.Range(0, 3), _currentLevelPreset.countRandomResource);
        //            }

        //            if (_currentLevelPreset.addRareResource)
        //            {
        //                _inventory.AddIngredientWithIndex(UnityEngine.Random.Range(4, 8), _currentLevelPreset.countRandomResource);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Debug.LogWarning("Не указано количество отдаваемых ресурсв");
        //    }
        //}       
    }

    private void CheckCompleteLevel()
    {
        if (_moneyTask.MoneyTaskComplete)
        {
            _gameManager.CompleteLevel();
            OnLevelComplete?.Invoke();
        }

        //if (_currentLevelPreset.isCollectIngredient)
        //{
        //    if (_moneyTask.MoneyTaskComplete && _collectable.InventroryComplete)
        //    {
        //        _gameManager.CompleteLevel();
        //        OnLevelComplete?.Invoke();
        //        return;
        //    }
        //}
        //else
        //{
            
        //}         
    }

    public void CheckContrabandLevel()
    {
        if (_currentLevelPreset.isContrabandLevel)
        {
            _contrabandSystem.InitContrabandPotion(_currentLevelPreset.contrabandTimer,_currentLevelPreset.contrabadPotionChance);
            _globalTaskView.AddContrabandText(_contrabandSystem.ContrabandPotion.PotionName);
        }       
    }

    private void OnDisable()
    {
        _priceSetter?.ResetPrice();
    }
}
