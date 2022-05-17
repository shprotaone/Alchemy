using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TableSystem : MonoBehaviour,IDropHandler
{
    private const string bottleTag = "Bottle";

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.CompareTag(bottleTag))
        {
            eventData.pointerDrag.transform.SetParent(eventData.pointerEnter.transform);
        }           
    }
}
