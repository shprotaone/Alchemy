using DG.Tweening;
using System.Linq;
using TMPro;
using UnityEngine;

public class FullBottleSlot : MonoBehaviour,ISlot
{
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private bool _isFree;

    [SerializeField] private BottleModel _bottlesInSlot;

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
                else if(Enumerable.SequenceEqual(_bottlesInSlot?.Data.Labels, bottle?.Data.Labels))
                {
                    bottle.SetPosition(this.transform);
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

        DOVirtual.DelayedCall(0.5f, () =>
        {
            if (transform.childCount == 1)
            {
                SetFreeSlot();
            }
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
            if(i == 1)
            {
                transform.GetChild(i).TryGetComponent(out BottleModel bottle);
                bottle.transform.DOScale(bottle.View.standartScale, 0.5f).OnStart(() => bottle.transform.gameObject.SetActive(true)); 
            }
            else if (transform.GetChild(i).TryGetComponent(out BottleModel bottle))
            {
                bottle.transform.DOScale(0, 0.5f).OnComplete(() =>bottle.transform.gameObject.SetActive(false));
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
}
