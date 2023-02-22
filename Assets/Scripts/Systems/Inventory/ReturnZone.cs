using DG.Tweening;
using UnityEngine;

public class ReturnZone : MonoBehaviour
{
    [SerializeField] private BottleInventory _bottleInventory;
    [SerializeField] private TradeSystem _tradeSystem;

    private bool _isCall;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out BottleModel bottle) && !_isCall)
        {
            if (bottle.gameObject.layer != Layer.Dragging)
            {
                _isCall = true;                
                bottle.SetPosition(_bottleInventory.GetSlot(bottle.PotionInBottle).transform);

                DOVirtual.DelayedCall(1f, () =>
                {
                    _isCall = false;
                });
            }
        }
    }
}
