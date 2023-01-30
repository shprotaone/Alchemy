using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class FullBottleSlot : MonoBehaviour,ISlot
{
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private bool _isFree;

    private BottleModel _bottleInSlot;
    private bool _inThisSlot;
    public BottleModel BottleInSlot => _bottleInSlot;
    public int Count { get; private set; }
    public bool IsFree => _isFree;

    public Transform Transform => this.transform;

    private void Start()
    {
        _isFree = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out BottleModel bottle) )
        {
            if (IsFree)
            {
                SetSlot(bottle);
            }
            else if (Enumerable.SequenceEqual(_bottleInSlot?.Data.Labels, bottle?.Data.Labels))
            {
                bottle.SetSlot(this.transform);
                CheckSlot();
            }
            ScaleBottles();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BottleModel bottle))
        {
            ScaleBottles();
        }
    }

    private void SlotBehaviour(ISlot slot)
    {
        if (slot is TradeSlot trade)
        {
            trade.SetSlotFree();
            //trade.CheckSlot();
        }
        else
        {
            slot?.CheckSlot();
        }
    }

    public void SetSlot(BottleModel bottle)
    {
        bottle.SetSlot(this.transform);

        if (_bottleInSlot == null)
        {
            _bottleInSlot = bottle;
        }
        _isFree = false;

        SlotBehaviour(bottle._prevSlot);
        CheckSlot();
    }

    public void CheckSlot()
    {
        Count = transform.childCount - 1;

        if (Count == 0)
        {
            _isFree = true;
        }
        else if (Count <= 1)
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
            if(i == 1)
            {
                transform.GetChild(i).TryGetComponent(out BottleModel bottle);
                bottle.transform.DOScale(bottle.View.standartScale, 0.5f).OnStart(() => AfterEnable(bottle)); 
            }
            else if (transform.GetChild(i).TryGetComponent(out BottleModel bottle))
            {
                bottle.transform.DOScale(0, 0.5f).OnComplete(() =>bottle.gameObject.SetActive(false));
            }
        }
    }

    private void AfterEnable(BottleModel bottle)
    {
        _inThisSlot = true;
        bottle.gameObject.SetActive(true);

        DOVirtual.DelayedCall(1, () => _inThisSlot = false);
    }

    private void SetFreeSlot()
    {
        _isFree = true;
        _bottleInSlot = null;
    }

    public void ResetSlot()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            BottleModel bottle = GetComponentInChildren<BottleModel>();

            if (bottle != null)
            {
                bottle.DestroyBottle();
                SetFreeSlot();
            }

            CheckSlot();
        }
        
    }
}  