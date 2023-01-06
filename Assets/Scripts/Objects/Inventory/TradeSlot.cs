using DG.Tweening;
using UnityEngine;

public class TradeSlot : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;

    private TradeSystem _tradeSystem;
    private Bottle _bottleInSlot;

    public Bottle BottleInSlot => _bottleInSlot;
    public bool IsFree { get; private set; }

    private void Start()
    {
        IsFree = true;
        _tradeSystem = GetComponentInParent<TradeSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bottle bottle) && IsFree && BottleInSlot != bottle)
        {
            bottle.SetPosition(this.transform);
            bottle.transform.SetParent(this.transform);

            _bottleInSlot = bottle;
            _bottleInSlot.OnDropped += CheckSlot;
            FillLabelInTradeSystem();
        }
    }

    private void FillLabelInTradeSystem()
    {       
        _tradeSystem.FillLabels(BottleInSlot.Labels);        
        _collider.enabled = false;
        DelayCollider();
    }

    private void CheckSlot()
    {     
             if (this.transform.childCount == 0)
             {
                 IsFree = true;

                 foreach (var labelType in BottleInSlot.Labels)
                 {
                     _tradeSystem.DeleteLabel(labelType);
                 }

                 _bottleInSlot.OnDropped -= CheckSlot;
                 _bottleInSlot = null;
                 
             }

             if (_bottleInSlot != null)
             {
                 IsFree = false;               
             }

             _bottleInSlot?.SetTradeScale();
    }

    private void DelayCollider()
    {
        DOVirtual.DelayedCall(1, () => _collider.enabled = true);
    }

    public void Reset()
    {
        IsFree = true;      
        BottleInSlot?.DestroyBottle();
    }
}
