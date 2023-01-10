using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeSlot : MonoBehaviour,ISlot
{
    [SerializeField] private Collider2D _collider;
    [SerializeField] private bool _isFree;

    private List<PotionLabelType> _labels;
    private TradeSystem _tradeSystem;
    private Bottle _bottleInSlot;

    public Bottle BottleInSlot => _bottleInSlot;
    public bool IsFree => _isFree;

    private void Start()
    {
        _isFree = true;
        _tradeSystem = GetComponentInParent<TradeSystem>();
        _labels = new List<PotionLabelType>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bottle bottle) )
        {
            if (IsFree)
            {
                bottle.SetPosition(this.transform);
                CheckSlot();
            }
            else if (bottle == BottleInSlot)
            {
                return;
            }
            
        }
    }

    public void SetSlot(Bottle bottle)
    {
        _labels.AddRange(bottle.Labels);
        
        _tradeSystem.FillLabels(_labels);
        _bottleInSlot = bottle;
        _isFree = false;
        _collider.enabled = false;
        DelayCollider();
    }

    public void CheckSlot()
    {     
       StartCoroutine(CheckDelay());
    }

    private IEnumerator CheckDelay()
    {
        yield return new WaitForSeconds(0.3f);

        if (transform.childCount == 0)
        {           
            _isFree = true;

            foreach (var labelType in _labels)
            {
                _tradeSystem.DeleteLabel(labelType);
            }

            _labels.Clear();
            _bottleInSlot = null;
        }

        _bottleInSlot?.SetTradeScale();
    }

    private void DelayCollider()
    {
        DOVirtual.DelayedCall(1, () => _collider.enabled = true);
    }

    public void ResetSlot()
    {
        _isFree = true;      
        _labels.Clear();
        BottleInSlot?.DestroyBottle();
    }

    public void SetSlotFree()
    {
        if(_bottleInSlot == null)
        {
            _isFree = true;           
        }

        CheckSlot();
        _tradeSystem.CalculateReward();
    }
}
