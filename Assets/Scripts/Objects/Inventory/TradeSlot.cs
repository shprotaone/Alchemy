using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TradeSlot : MonoBehaviour,ISlot
{
    [SerializeField] private List<BottleModel> _bottlesInSlot;
    [SerializeField] private BottlePOS[] _bottlePos;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private BottleInventory _bottleInventory;

    private TradeSystem _tradeSystem;
    
    private bool _isFree;
    private bool _colliderDelay;

    public List<BottleModel> BottlesInSlot => _bottlesInSlot;
    public bool IsFree => _isFree;   //проверка что все слоты заполнены

    public Transform Transform => this.transform;

    public void Init(TradeSystem tradeSystem)
    {
        _tradeSystem = tradeSystem;
        _bottlesInSlot = new List<BottleModel>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BottleModel bottle))
        {
            SetSlot(bottle);
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BottleModel bottle))
        {
            //удаление из списка бутылок в слоте.
            _bottlesInSlot.Remove(bottle);
            DeleteDraggableBottleFromSlot(bottle);
            //просчет оставшихся лейблов в слоте. 
            DeleteLabel(bottle);
        }
    }

    private void DeleteDraggableBottleFromSlot(BottleModel bottle)
    {
        foreach (var slot in _bottlePos)
        {
            if(slot.Bottle != null && slot.Bottle == bottle)
            {
                slot.ResetSlot();
            }
        }
    }

    private void DeleteLabel(BottleModel bottle)
    {
        if(bottle.Data != null) _tradeSystem.DeleteLabel(bottle.Data.Labels);
    }

    private ISlot GetSlot(BottleModel bottle)
    {
        foreach (var slot in _bottlePos)
        {
            if (slot.IsFree)
            {
                slot.SetSlot(bottle);
                return slot.GetSlot();
            }
        }
        Debug.Log("Свободные позиции не найдены");
        return null;
    }

    private void AddBottleToList(BottleModel bottle)
    {
        if(_bottlesInSlot.Count < 3)
        {
            _bottlesInSlot.Add(bottle);
            _tradeSystem.FillLabels(bottle.Data.Labels);          
        }
    }

    public void SetSlot(BottleModel bottle)
    {
        ISlot slot = GetSlot(bottle);
        //Присвоение бутылке позициии свободного слота
        bottle.SetSlot(slot.Transform);
        //Добавление в список текущих бутылок
        AddBottleToList(bottle);
        //Просчет стоимости текщих лейблов из бутылок
        _tradeSystem.CalculateReward();

        _colliderDelay = true;
        DOVirtual.DelayedCall(1, () => _colliderDelay = false);
    }

    public void SetSlotFree()
    {
        _isFree = true;
    }

    public void ResetAllBottlesAfterTrade()
    {
        foreach (var slot in _bottlePos)
        {
            slot.ResetSlotAfterTrade();
        }
    }

    public void ResetSlot()
    {
        
    }

    public void ReturnBottles()
    {
        foreach (var bottle in _bottlesInSlot)
        {
            bottle.ReturnToSlot();
        }
    }

    public void CheckSlot()
    {
        
    }
}
