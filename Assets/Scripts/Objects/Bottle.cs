using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bottle : MonoBehaviour
{
    private const float moveSpeed = 1;

    [SerializeField] private Image _fullBottle;
   
    private CanvasGroup _canvasGroup;
    private Potion _potionInBottle;
    private Table _currentTable;

    private bool _isFull;
    public bool IsFull => _isFull;
    public Potion PotionInBottle => _potionInBottle;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.blocksRaycasts = true;
        _potionInBottle = GetComponent<Potion>();
    }

    public void Movement(Table table,Vector2 startPos)
    {
        print(startPos);

        _currentTable = table;
        transform.SetParent(table.transform);

        if(_isFull)
        {
            transform.DOMove(_currentTable.transform.position, moveSpeed, false).OnComplete(PutOnTable); // переделать все на rectTransform
        }
        else
        {
            transform.DOMove(startPos, moveSpeed, false).OnComplete(PutOnTable);
        }                   
    }

    private void PutOnTable()
    {
        _currentTable.RefreshPos();
        //transform.position = _currentTable.transform.position;        
    }

    public void FillBottle(Color color)
    {
        _fullBottle.enabled = true;
        _fullBottle.color = color;
        _isFull = true;
    }

    public void FillPotionInBottle(Potion potion)
    {
        _potionInBottle = potion;
        print("From Bottle" + _potionInBottle.PotionName);
    }

    public void ResetBottle()
    {
        _isFull = false;
        _fullBottle.enabled = false;
        _potionInBottle = null;
    }
}
