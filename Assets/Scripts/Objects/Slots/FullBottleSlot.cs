using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class FullBottleSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private bool _isFree;

    private Bottle _bottleInSlot;
    public bool IsFree => _isFree;
    public Bottle BottleInSlot => _bottleInSlot;
    public int Count { get; private set; }

    private void Start()
    {
        _isFree = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Bottle bottle))
        {
            if(IsFree)
            {
                bottle.OnDropped += SetFreeSlot;
                bottle.OnDropped += CheckChild;

                bottle.SetPosition(this.transform);     //необходимо при отпускании между слотами

                _isFree = false;
                _bottleInSlot = bottle;
            }
            else
            {
                if (Enumerable.SequenceEqual(_bottleInSlot.Labels, bottle.Labels))
                {
                    bottle.SetPosition(this.transform);
                }
            }            
        }
    }

    public void CheckChild()
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

    private void SetFreeSlot()
    {
        _isFree = true;
        if(_bottleInSlot != null)
        {
            _bottleInSlot.OnDropped -= SetFreeSlot;
            _bottleInSlot.OnDropped -= CheckChild;
            DeleteBottles();
        }  
    }

    private void DeleteBottles()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Bottle bottle = GetComponentInChildren<Bottle>();

            if(bottle != null)
            {
                bottle.DestroyBottle();
                CheckChild();
            }
        }
    }

    public void Reset()
    {
        SetFreeSlot();
    }
}
