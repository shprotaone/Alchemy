using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class FullBottleSlot : MonoBehaviour,ISlot
{
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private bool _isFree;
    [SerializeField] private bool isCheck = false;

    [SerializeField] private BottleModel _bottlesInSlot;

    private int _calls;
    public BottleModel BottlesInSlot => _bottlesInSlot;
    public int Count { get; private set; }
    public bool IsFree => _isFree;

    private void Start()
    {
        _isFree = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out BottleModel bottle))
        {           
                if (IsFree)
                {
                    SetSlot(bottle,true);
                }
                else if(Enumerable.SequenceEqual(_bottlesInSlot.BottleData.Labels, bottle.BottleData.Labels))
                {
                    bottle.SetPosition(this.transform);
                    _bottlesInSlot = bottle;
                    CheckSlot();
                }
        }
    }

    public void SetSlot(BottleModel bottle,bool IsDraggable)
    {
        if (IsDraggable)
        {
            bottle.SetPosition(this.transform);            
        }

        _isFree = false;

        if (_bottlesInSlot == null)
        {
            _bottlesInSlot = bottle;
        }
        //бутылки не удаляются до конца уровня
        SlotBehaviour(bottle._prevSlot);
        CheckSlot();
        //HidePrevBottle();
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
        Count = transform.childCount-1;

        if(Count <= 1)
        {
            _countText.text = " ";
        }
        else
        {
            _countText.text = Count.ToString();
        }

        if (transform.childCount == 1)
        {
            SetFreeSlot();           
        }
    }

    private void SetFreeSlot()
    {
        _isFree = true;
        _bottlesInSlot = null;
    }

    public void ResetSlot()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            BottleModel bottle = GetComponentInChildren<BottleModel>();

            if (bottle != null)
            {
                bottle.DestroyBottle();
                CheckSlot();
            }
        }
        SetFreeSlot();
    }

    public void SetSlot(BottleModel bottle)
    {
        //throw new NotImplementedException();
    }
}
