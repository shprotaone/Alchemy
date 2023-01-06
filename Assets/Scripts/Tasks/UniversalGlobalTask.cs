using System;
using TMPro;
using UnityEngine;

public class UniversalGlobalTask : MonoBehaviour
{
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
        //_globalTaskView.SetLevelTaskText(taskText);              
    }
}
