using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeSystemV2 : MonoBehaviour,ISlot
{
    [SerializeField] private List<BottleModel> _bottles;
    [SerializeField] private List<Transform> _slotTransform;

    public bool IsFree => throw new System.NotImplementedException();

    public Transform Transform => throw new System.NotImplementedException();

    private void Start()
    {
        _bottles = new List<BottleModel>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out BottleModel bottle))
        {
            bool duplicate = CheckDoubleBottle(bottle);

            if (!duplicate)
            {
                _bottles.Add(bottle);
                bottle.SetPosition(GetPositon());
            }
        }
    }

    private bool CheckDoubleBottle(BottleModel bottle)
    {
        foreach (var item in _bottles)
        {
            if (item == bottle) return true;
        }

        return false;
    }

    private Transform GetPositon()
    {
        foreach (var item in _slotTransform)
        {
            if (item.childCount == 0) return item.transform;
        }

        return null;
    }

    public void DeleteBottle(BottleModel bottle)
    {
        _bottles.Remove(bottle);
    }

    public void CheckSlot()
    {
        //throw new System.NotImplementedException();
    }

    public void SetSlot(BottleModel bottle)
    {
        
    }

    public void CleanSLotAfterDraggable()
    {
        //throw new System.NotImplementedException();
    }

    public void SetSlot(BottleModel bottle, bool IsDraggable)
    {
        //throw new System.NotImplementedException();
    }
}
