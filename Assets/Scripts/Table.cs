using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Table : MonoBehaviour,IDropHandler
{
    [SerializeField] private bool _fullPotionTable;
    private const string bottleTag = "Bottle";
    private GridLayoutGroup _gridLayout;
    private Vector2 _offset;

    public bool FullPotionTable => _fullPotionTable;
    public Vector2 Offset => _offset;

    private void Start()
    {
        _gridLayout = GetComponent<GridLayoutGroup>();
        _offset = _gridLayout.cellSize;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.CompareTag(bottleTag))
        {
            eventData.pointerDrag.transform.SetParent(eventData.pointerEnter.transform);
        }           
    }

    public void RefreshPos()
    {
        _gridLayout.enabled = false;
        _gridLayout.enabled = true;
    }
}
