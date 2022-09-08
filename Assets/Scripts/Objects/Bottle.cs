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
    private BottleStorage _bottleStorage;

    private Tween _tween;

    private bool _isFull;
    public bool IsFull => _isFull;
    public Potion PotionInBottle => _potionInBottle;

    private void Start()
    {
        _currentTable = GetComponentInParent<Table>();
        _tableManager = GetComponentInParent<TableManager>();
    }

    public void InitBottle(BottleStorage storage)
    {
        _potionInBottle = new Potion();
        _bottleStorage = storage;
    }

    public void Movement()
    {
        _tween = transform.DOMove(_currentTable.transform.position, moveSpeed, false).OnStart(SortInFullTable).OnComplete(ReturnToStorage);             
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

    private void SortInFullTable()
    {
        transform.SetParent(_currentTable.transform);
        _currentTable.SortBottlePosition();           
            
        _collider.enabled = true;
    }

    private void ReturnToStorage()
    {
        if (!_isFull)
        {
            _bottleStorage.IncreaseAmount();
            Destroy(gameObject);
        }
    }

    public void ResetBottle()
    {
        _tween.Kill(true);

        Destroy(this.gameObject);
    }

    public void Action()
    {
        
    }
}
