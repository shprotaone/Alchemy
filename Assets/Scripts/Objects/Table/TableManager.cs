using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    private Table[] _tables;
    [SerializeField] private Table _emptyPotionTable;
    [SerializeField] private Table _fullPotionTable;

    public Table EmptyPotionTable => _emptyPotionTable;
    public Table FullPotionTable => _fullPotionTable;

    private void Start()
    {
        _tables = GetComponentsInChildren<Table>();

        foreach (var item in _tables)
        {
            if (item.FullPotionTable)
            {
                _fullPotionTable = item;
            }
            else
            {
                _emptyPotionTable = item;
            }
        }
    }    
}
