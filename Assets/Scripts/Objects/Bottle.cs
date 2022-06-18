using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    private const float moveSpeed = 1;

    [SerializeField] private SpriteRenderer _fullBottle;
    [SerializeField] private Transform _effectTransform;

    private GameObject _effect;
    private Wobble _wobble;
    private Potion _potionInBottle;
    private Table _currentTable;

    private bool _isFull;
    public bool IsFull => _isFull;
    public Potion PotionInBottle => _potionInBottle;

    private void Start()
    {
        _potionInBottle = GetComponent<Potion>();
        _wobble = GetComponentInChildren<Wobble>();
        _currentTable = GetComponentInParent<Table>();
    }

    public void Movement()
    {    
        transform.DOMove(_currentTable.SetPositionForBottle(), moveSpeed, false).OnComplete(SetBottleParent);
    }

    public void FillWaterInBottle(Color color)
    {
        _fullBottle.enabled = true;
        _wobble.ChangeColor(color);
        _isFull = true;
    }

    public void FillPotionInBottle(Potion potion)
    {
        _potionInBottle.SetNamePotion(potion.PotionName);
        _currentTable = _currentTable.GetComponentInParent<TableManager>().FullPotionTable;

        if(potion.Rarity == ResourceRarity.rare)
        {
            _effect = Instantiate(potion.Effect,_effectTransform.position, Quaternion.identity);           
            _effect.transform.SetParent(transform);
            _effect.transform.localScale = new Vector3(1, 1, 0);
        }
        print("From Bottle" + _potionInBottle.PotionName);
    }

    public void SetTable()
    {
        _currentTable = _currentTable.GetComponentInParent<TableManager>().EmptyPotionTable;
    }

    private void SetBottleParent()
    {
        transform.SetParent(_currentTable.transform);
    }

    public void ResetBottle()
    {
        _isFull = false;
        _fullBottle.enabled = false;

        SetTable();

        Destroy(_effect);
        Movement();
    }
}
