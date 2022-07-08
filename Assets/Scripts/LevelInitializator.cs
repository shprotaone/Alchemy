using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitializator : MonoBehaviour
{
    public const int stockAmount = 5;
    public const int stockBottleAmount = 2;

    [SerializeField] private TutorialSystem _tutorialSystem;
    [SerializeField] private ShopSystem _shopSystem;
    [SerializeField] private EventCounter _eventDialogCounter;
    [SerializeField] private CameraMovement _startCameraPos;
    [SerializeField] private LevelPreset _levelPreset;
    [SerializeField] private DialogManager _dialogManager;
    [SerializeField] private TaskSystem _taskSystem;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private bool _tutorial;

    public bool Tutorial => _tutorial;

    private void Start()
    {
        if (_tutorial)
        {
            DragController.instance.ObjectsInterractable(false);

            StartCoroutine(_tutorialSystem.StartTutorialDelay());
            _taskSystem.TutorialMode(true);
            _eventDialogCounter.SetEventCounterArray(_levelPreset.eventCount);
            _inventory.FillInventory(0);
            _inventory.AddBottle(stockBottleAmount);
        }
        else
        {
            _inventory.FillInventory(stockAmount);
            _inventory.AddBottle(stockBottleAmount);
        }

        _dialogManager.SetDialogArray(_levelPreset.dialog);        

        _startCameraPos.SetStartPosition(_levelPreset.startWindow);
        _taskSystem.SetTaskType(_levelPreset.rareTask);
        
    }  
}
