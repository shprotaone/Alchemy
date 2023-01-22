using UnityEngine;

public class TradeSlot : MonoBehaviour,ISlot
{
    [SerializeField] private Collider2D _collider;
    [SerializeField] private bool _isFree;

    private TradeSystem _tradeSystem;
    private BottleModel _bottleInSlot;
    private bool _isAdded = false;

    public BottleModel BottleInSlot => _bottleInSlot;
    public bool IsFree => _isFree;
    

    private void Start()
    {
        _isFree = true;
        _tradeSystem = GetComponentInParent<TradeSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BottleModel bottle))
        {
            if(_bottleInSlot == bottle)
            {
                _bottleInSlot = bottle;
                bottle.transform.SetParent(this.transform);
            }
            else
            {                
                if (IsFree)
                {
                    SetSlot(bottle, true);
                    _isAdded = true;
                }
            }          
        }
    }

    public void SetSlot(BottleModel bottle, bool IsDraggable)
    {
        if (!_isAdded)  //вторая проверка
        {
            bottle.SetPosition(this.transform);
            _isFree = false;

            _tradeSystem.FillLabels(bottle.Data.Labels);
            _bottleInSlot = bottle;           
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
        ColliderController();       
    }

    public void SetSlotFree()
    {
        _tradeSystem.DeleteLabel(_bottleInSlot.Data.Labels);
        _isAdded = false;
        _isFree = true;
        _bottleInSlot = null;
        ColliderController();
    }

    public void ResetSlot()
    {
        _isFree = true;
        _isAdded = false;       
        _bottleInSlot.DestroyBottle();
        _bottleInSlot = null;
        ColliderController();
    }
    private void ColliderController()
    {
        if (IsFree)
        {
            _collider.enabled = true;
            _tradeSystem.CalculateReward();
        }
        else
        {
            _collider.enabled = false;
        }
    }
}
