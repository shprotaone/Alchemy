using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableObjectController : MonoBehaviour
{
    [SerializeField] private Slot[] _draggableGO;
    [SerializeField] private ShopController _shopGo;
    public void SetItteract(bool flag)
    {
        foreach (var item in _draggableGO)
        {
            item.SetDraggable(flag);
        }
    }
}
