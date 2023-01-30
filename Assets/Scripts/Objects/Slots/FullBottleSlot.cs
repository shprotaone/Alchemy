using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class FullBottleSlot : MonoBehaviour,ISlot
{
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BottleModel bottle))
        {
            if (IsFree)
            {
                SetSlot(bottle, true);
            }
            else if (_bottlesInSlot != null)
            {
                bool isEqual = _bottlesInSlot.Data.Labels.SequenceEqual(bottle.Data.Labels);
                if(isEqual)
                {
                    bottle.SetPosition(this.transform);
                    CheckSlot();
                    bottle.SetForcePosition(this.transform.position);       //сделано специально, так как при маленьком расстоянии бутылка не успевает долететь
                }
                ScaleBottles();
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BottleModel bottle))
        {
            ScaleBottles();
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

        if (transform.childCount == 1)
        {
            SetFreeSlot();
        }
        DOVirtual.DelayedCall(0.2f, () =>       //задержка 0,2f была сделана для того, чтобы не сразу определялся пустой слот или нет
        {                                       
                                                
        });
    }

    public void CheckCountSlot()
    {
        Count = transform.childCount - 1;

        if (Count <= 1)
        {
            _countText.text = " ";
        }
        else
        {
            _countText.text = Count.ToString();
        }
    }

    private void ScaleBottles()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == 1)
            {
                transform.GetChild(i).TryGetComponent(out BottleModel bottle);
                bottle.transform.DOScale(bottle.View.standartScale, 0.1f).OnStart(() => bottle.transform.gameObject.SetActive(true));
            }
            else if (transform.GetChild(i).TryGetComponent(out BottleModel bottle))
            {
                bottle.transform.DOScale(0, 0.3f).OnComplete(() => bottle.transform.gameObject.SetActive(false));       
            }
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
        
    }
}  