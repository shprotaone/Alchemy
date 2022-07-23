using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour,IAction
{
    private const float moveSpeed = 1;

    [SerializeField] private SpriteRenderer _fullBottle;
    [SerializeField] private SpriteRenderer _bottle;
    [SerializeField] private Transform _effectTransform;

    private GameObject _effect;
    private BoxCollider2D _collider;
    private Wobble _wobble;
    private Potion _potionInBottle;
    private TableManager _tableManager;
    private Table _currentTable;
    private NextCountHandler _nextCountHandler;

    private bool _isFull;
    private bool _firstFill = true;
    public bool IsFull => _isFull;
    public Potion PotionInBottle => _potionInBottle;

    private void Start()
    {
        _potionInBottle = GetComponent<Potion>();
        _wobble = GetComponentInChildren<Wobble>();
        _currentTable = GetComponentInParent<Table>();
        _collider = GetComponent<BoxCollider2D>();
        _tableManager = GetComponentInParent<TableManager>();

        _nextCountHandler = GetComponent<NextCountHandler>();
    }

    public void Movement()
    {        
        transform.DOMove(_currentTable.SetPositionForBottle(), moveSpeed, false).OnComplete(SetBottleParent);       
    }

    public void FillWaterInBottle(Color color)
    {
        if (_firstFill)
        {
            _nextCountHandler.DisableClickHerePrefab();
            _firstFill = false;
        }
        
        _fullBottle.enabled = true;
        _wobble.ChangeColor(color);
        _isFull = true;
    }

    public void FillPotionInBottle(Potion potion)
    {
        _potionInBottle.SetNamePotion(potion.PotionName);

        SetTable();

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
        if (_isFull)
            _currentTable = _tableManager.FullPotionTable;
        else
            _currentTable = _tableManager.EmptyPotionTable;
    }

    private void SetBottleParent()
    {
        transform.SetParent(_currentTable.transform);
        _collider.enabled = true;
    }

    public void ResetBottle()
    {
        _isFull = false;
        _fullBottle.enabled = false;

        SetTable();
        Destroy(_effect);
    }

    public void Action()
    {
        
    }
}
