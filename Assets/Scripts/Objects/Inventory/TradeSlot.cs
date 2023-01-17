using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeSlot : MonoBehaviour,ISlot
{
    [SerializeField] private Collider2D _collider;
    [SerializeField] private bool _isFree;

    //private List<PotionLabelType> _labels;
    private TradeSystem _tradeSystem;
    private BottleModel _bottleInSlot;
    private bool _isAdded = false;
    private bool _slotIsChanged = false;

    public BottleModel BottleInSlot => _bottleInSlot;
    public bool IsFree => _isFree;
    

    private void Start()
    {
        _isFree = true;
        _tradeSystem = GetComponentInParent<TradeSystem>();
        //_labels = new List<PotionLabelType>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BottleModel bottle))
        {
            if(_bottleInSlot == bottle)
            {
                _slotIsChanged = false;
                _bottleInSlot = bottle;
                bottle.transform.SetParent(this.transform);
            }
            else
            {
                _slotIsChanged = true;
                
                if (IsFree)
                {
                    SetSlot(bottle, true);
                    _isAdded = true;
                }
            }          
        }
    }

    public void SetSlot(BottleModel bottle, bool IsDraggable)
    {
        if (!_isAdded)  //вторая проверка
        {
            bottle.SetPosition(this.transform);
            _isFree = false;

            _tradeSystem.FillLabels(bottle.BottleData.Labels);
            _bottleInSlot = bottle;           
        }

        SlotBehaviour(bottle._prevSlot);
        CheckSlot();
    }

    private void SlotBehaviour(ISlot slot)
    {
        if (slot is TradeSlot trade)
        {
            trade.SetSlotFree();
            trade.CheckSlot();
        }
        else
        {
            slot?.CheckSlot();
        }
    }

    public void CheckSlot()
    {
        CheckFreeSlot();      
    }

    public void SetSlotFree()
    {
        _tradeSystem.DeleteLabel(_bottleInSlot.BottleData.Labels);
        _tradeSystem.CalculateReward();
        _isAdded = false;
        _isFree = true;
        _bottleInSlot = null;
    }

    public void ResetSlot()
    {
        _isFree = true;
        _isAdded = false;
        BottleInSlot.DestroyBottle();
    }

    public void CheckFreeSlot()
    {
           
    }   
}
