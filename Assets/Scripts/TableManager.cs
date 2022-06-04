using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public static TableManager instance;

    [SerializeField] private Table _emptyPotionTable;
    [SerializeField] private Table _fullPotionTable;
    private RectTransform _emptyPotionPosition;
    private RectTransform _fullPotionPosition;

    public Table EmptyPotionTable => _emptyPotionTable;
    public Table FullPotionTable => _fullPotionTable;

    public RectTransform EmptyPotionPosition => _emptyPotionPosition;
    public RectTransform FullPotionPosition => _fullPotionPosition;

    private void Awake()
    {
        _emptyPotionPosition = _emptyPotionTable.GetComponent<RectTransform>();
        _fullPotionPosition = _fullPotionTable.GetComponent<RectTransform>();

        if(instance == null)
        {
            instance = this;
        }
        else if( instance == this)
        {
            Destroy(gameObject);
        }
    }
}
