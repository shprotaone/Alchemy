using DG.Tweening;
using UnityEngine;

public class Bottle : MonoBehaviour,IAction
{
    private const float moveSpeed = 1;

    [SerializeField] private SpriteRenderer _fullBottle;
    [SerializeField] private SpriteRenderer _bottle;
    [SerializeField] private Transform _effectTransform;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private Wobble _wobble;

    private GameObject _effect;
    private Potion _potionInBottle;
    private Color _potionColor;
    private TableManager _tableManager;
    private Table _currentTable;

    private bool _isFull;
    public bool IsFull => _isFull;
    public Potion PotionInBottle => _potionInBottle;

    private void Start()        //убрать GetComponent
    {
        _potionInBottle = new Potion();

        _currentTable = GetComponentInParent<Table>();
        _tableManager = GetComponentInParent<TableManager>();
    }

    public void Movement()
    {               
        transform.DOMove(_currentTable.transform.position, moveSpeed, false).OnStart(Test).OnComplete(EnableCollider);       
    }

    public void FillWaterInBottle(Color color)
    {       
        _potionColor = color;
        _fullBottle.enabled = true;
        _wobble.ChangeColor(_potionColor);
        _isFull = true;
    }

    public void FillPotionInBottle(Potion potion)
    {
        _potionInBottle.SetNamePotion(potion.PotionName);

        SetTable();

        if(potion.Rarity == ResourceRarity.rare)
        {
            _effect = Instantiate(potion.Effect,_effectTransform.position, Quaternion.identity);
            _effect.GetComponentInChildren<EffectChangeColor>().ChangeParticleColor(_potionColor);
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

    private void Test()
    {
        transform.SetParent(_currentTable.transform);
        _currentTable.SortBottlePosition();
    }

    private void EnableCollider()
    {
        _collider.enabled = true;
    }

    public void ResetBottle()
    {
        _isFull = false;
        _fullBottle.enabled = false;

        SetTable();
        EnableCollider();
        Destroy(_effect);
    }

    public void Action()
    {
        
    }
}
