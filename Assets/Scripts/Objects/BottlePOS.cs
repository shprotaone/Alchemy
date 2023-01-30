using DG.Tweening;
using UnityEngine;

public class BottlePOS : MonoBehaviour,ISlot
{
    private bool _isFree = true;
    public bool IsFree => _isFree;
    public BottleModel Bottle { get; set; }
    public Transform Transform => this.transform;

    public void CheckSlot()
    {
        
    }

    public ISlot GetSlot()
    {
        _isFree = false;        
        return this;
    }

    public void ResetSlot()
    {
        if (!_isFree)
        {                       
            _isFree = true;
        }    
    }

    public void ResetSlotAfterTrade()
    {
        if (Bottle != null) Bottle.DestroyBottle();
    }

    public void SetSlot(BottleModel bottle)
    {
        Bottle = bottle;
    }
}
