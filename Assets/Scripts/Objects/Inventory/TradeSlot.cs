using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TradeSlot : MonoBehaviour,ISlot
{
    public event Action<int> OnSell;

    [SerializeField] private List<BottleModel> _bottlesInSlot;
    [SerializeField] private BottlePOS[] _bottlePos;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private BottleInventory _bottleInventory;

    private TradeSystem _tradeSystem;    
    private bool _isFree;

    public List<BottleModel> BottlesInSlot => _bottlesInSlot;
    public bool IsFree => _isFree;   //�������� ��� ��� ����� ���������

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
            //�������� �� ������ ������� � �����.
            _bottlesInSlot.Remove(bottle);
            DeleteDraggableBottleFromSlot(bottle);
            //������� ���������� ������� � �����. 
            DeleteLabel(bottle);
        }
    }

    private void DeleteDraggableBottleFromSlot(BottleModel bottle)
    {
        foreach (var slot in _bottlePos)
        {
            if(slot.Bottle != null && slot.Bottle == bottle)
            {
                slot.CleanSLotAfterDraggable();
            }
        }
    }

    private void DeleteLabel(BottleModel bottle)
    {
        if(bottle.PotionInBottle != null) _tradeSystem.DeleteLabel(bottle.PotionInBottle.Labels);
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
        Debug.Log("��������� ������� �� �������");
        return null;
    }

    private void AddBottleToList(BottleModel bottle)
    {
        if(_bottlesInSlot.Count < 3)
        {
            _bottlesInSlot.Add(bottle);
            _tradeSystem.FillLabels(bottle.PotionInBottle.Labels);          
        }
    }

    public void SetSlot(BottleModel bottle)
    {
        ISlot slot = GetSlot(bottle);
        //���������� ������� �������� ���������� �����
        bottle.SetPosition(slot.Transform);
        //���������� � ������ ������� �������
        AddBottleToList(bottle);
        //������� ��������� ������ ������� �� �������
        _tradeSystem.CalculateReward();

        //_colliderDelay = true;
        //DOVirtual.DelayedCall(1, () => _colliderDelay = false);
    }

    public void SetSlotFree()
    {
        _isFree = true;
    }

    public void ResetAllBottlesAfterTrade()
    {
        OnSell?.Invoke(_bottlesInSlot.Count);
        foreach (var slot in _bottlePos)
        {
            slot.ResetSlotAfterTrade();
        }
    }

    public void CleanSLotAfterDraggable()
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
