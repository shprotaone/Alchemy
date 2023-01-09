using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class FullBottleSlot : MonoBehaviour,ISlot
{
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private bool _isFree;

    private Bottle _bottleInSlot;
    public Bottle BottleInSlot => _bottleInSlot;
    public int Count { get; private set; }
    public bool IsFree => _isFree;

    private void Start()
    {
        _isFree = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Bottle bottle))
        {
            if (IsFree)
            {
                SetSlot(bottle);
            }
            else
            {
                if (Enumerable.SequenceEqual(_bottleInSlot.Labels, bottle.Labels))
                {
                    bottle.SetPosition(this.transform);
                    
                }
            }

            bottle.OnDropped += CheckSlot;
            bottle.StandartSize();
        }
    }

    public void SetSlot(Bottle bottle)
    {
        bottle.SetPosition(this.transform);     //необходимо при отпускании между слотами        

        _isFree = false;
        _bottleInSlot = bottle;
        CheckSlot();
    }

    public void CheckSlot()
    {
        Count = transform.childCount - 1;

        if(Count <= 1)
        {
            _countText.text = " ";
        }
        else
        {
            _countText.text = Count.ToString();
        }       

        if(transform.childCount == 1)
        {
            SetFreeSlot();
        }
    }

    private void SetFreeSlot()
    {
        _isFree = true; 
        _bottleInSlot = null;
    }

    public void ResetSlot()
    {
        if(_bottleInSlot != null)
        {
            _isFree = true;

            for (int i = 0; i < transform.childCount; i++)
            {
                Bottle bottle = GetComponentInChildren<Bottle>();

                if (bottle != null)
                {
                    bottle.DestroyBottle();
                    CheckSlot();
                }
            }
        }
       
    }
}
