using System;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObjectController : MonoBehaviour
{
    [SerializeField] private MenuPanelController[] _menuPanelController;

    [SerializeField] private GameObject[] _interractGo;
    [SerializeField] private List<IInterract> _interract;
    [SerializeField] private BottleInventory _bottleInventory;

    private void Awake()
    {
        _interract = new List<IInterract>();

        for (int i = 0; i < _menuPanelController.Length; i++)
        {
            _menuPanelController[i].OnInterract += SetInterract;
        }
        
        for (int i = 0; i < _interractGo.Length; i++)
        {
            _interractGo[i].TryGetComponent(out IInterract component); ;

            _interract.Add(component);
        }

        //LevelInitializator.OnLevelEnded += DisableObjects;
        //LevelInitializator.OnLevelStarted += EnableObjects;
        //SetInterract(true);
    }

    private void EnableObjects()
    {
        foreach (var slot in _bottleInventory.Slots)
        {
            slot.BottlesInSlot?.SetInterract(true);
        }
    }

    private void DisableObjects()
    {
        foreach (var slot in _bottleInventory.Slots)
        {
            slot.BottlesInSlot?.SetInterract(false);
        }
    }

    public void SetInterract(bool flag)
    {
        if (flag) EnableObjects();
        else DisableObjects();

        foreach (var item in _interract)
        {
            item.SetInterract(flag);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _menuPanelController.Length; i++)
        {
            _menuPanelController[i].OnInterract -= SetInterract;
        }
    }
}
