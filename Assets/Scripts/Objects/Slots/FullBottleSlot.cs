using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class FullBottleSlot : MonoBehaviour,ISlot
{
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private bool _isFree;

    private List<Bottle> _bottlesInSlot;
    public List<Bottle> BottlesInSlot => _bottlesInSlot;
    public int Count { get; private set; }
    public bool IsFree => _isFree;

    private void Start()
    {
        _isFree = true;
        _bottlesInSlot = new List<Bottle>();
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
                if (Enumerable.SequenceEqual(_bottlesInSlot[0].Labels, bottle.Labels))
                {
                    bottle.SetPosition(this.transform);
                    _bottlesInSlot.Add(bottle);
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
        _bottlesInSlot.Add(bottle);     //бутылки не удал€ютс€ до конца уровн€
        CheckSlot();

        //HidePrevBottle();
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

        if(transform.childCount == 1)
        {
            SetFreeSlot();
        }

    }

    private void SetFreeSlot()
    {
        _isFree = true;
        _bottlesInSlot.Clear();
    }

    public void ResetSlot()
    {
        if(_bottlesInSlot.Count > 0)
        {
            for (int i = 0; i < _bottlesInSlot.Count; i++)
            {
                Bottle bottle = GetComponentInChildren<Bottle>();

                if (bottle != null)
                {
                    bottle.DestroyBottle();
                    CheckSlot();
                }
            }
        }
        _bottlesInSlot.Clear();
        SetFreeSlot();
    }
}
