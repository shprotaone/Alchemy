using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class FullBottleSlot : MonoBehaviour,ISlot
{
    public List<BottleModel> Bottles;
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private bool _isFree;

    [SerializeField] private BottleModel _bottlesInSlot;

    public BottleModel BottleInSlot => _bottlesInSlot;
   
    public int Count { get; private set; }
    public bool IsFree => _isFree;

    public Transform Transform => this.transform;

    private void Start()
    {
        _isFree = true;
        Bottles = new List<BottleModel>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BottleModel bottle) && bottle.gameObject.layer != Layer.Dragging)
        {
            if (IsFree)
            {
                SetSlot(bottle, true);
            }
            else if(BottleInSlot != null)
            {
                bool isEqual = CheckContains(bottle);
                if(isEqual)
                {
                    SetSlot(bottle,true);                    
                    //bottle.SetForcePosition(this.transform.position);
                }
            }           
        }
    }

    public void SetSlot(BottleModel bottle, bool IsDraggable)
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

        AddBottle(bottle);
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
        CheckCountSlot();

        if (Bottles.Count == 0)
        {
            SetFreeSlot();
        }
    }

    public void CheckCountSlot()
    {
        Count = Bottles.Count;

        if (Count <= 1)
        {
            _countText.text = " ";
        }
        else
        {
            _countText.text = Count.ToString();
        }
    }

    public void ScaleBottles()
    {
        //BottleModel bottle;

        //for (int i = 0; i < Bottles.Count; i++)
        //{
        //    bottle = Bottles[i];

        //    if (i == 0)
        //    {              
        //        bottle.transform.DOScale(bottle.View.standartScale, 0.1f).OnStart(() => bottle.transform.gameObject.SetActive(true));
        //    }
        //    else
        //    {               
        //        bottle.transform.DOScale(0, 0.3f).OnComplete(() => bottle.transform.gameObject.SetActive(false));       
        //    }
        //}
    }

    private void SetFreeSlot()
    {
        _isFree = true;
        _bottlesInSlot = null;
        Bottles.Clear();
        CheckCountSlot();
    }

    private void AddBottle(BottleModel bottle)
    {
        if (!Bottles.Contains(bottle) && CheckContains(bottle))
        {
            Bottles.Add(bottle);
        }
    }

    private bool CheckContains(BottleModel bottle)
    {
        return this.BottleInSlot.Data.Labels.SequenceEqual(bottle.Data.Labels);
    }

    public void DeleteBottle(BottleModel bottle)
    {
        if (Bottles.Contains(bottle))
        {
            Bottles.Remove(bottle);
        }
    }

    public void ResetSlot()
    {
        for (int i = 0; i < Bottles.Count; i++)
        {
            Bottles[i].DestroyBottle();
        }
        Bottles.Clear();
        CheckSlot();
    }

    public void SetSlot(BottleModel bottle)
    {
        bottle.SetPosition(this.transform);
        
        _isFree = false;

        if (_bottlesInSlot == null)
        {
            _bottlesInSlot = bottle;
        }

        AddBottle(bottle);
        SlotBehaviour(bottle._prevSlot);
        CheckSlot();       
    }
}  