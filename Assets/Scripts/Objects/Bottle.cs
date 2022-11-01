using DG.Tweening;
using TMPro;
using UnityEngine;

public class Bottle : MonoBehaviour,IAction,IPooledObject
{
    private const float moveSpeed = 1;

    [SerializeField] private SpriteRenderer _fullBottle;
    [SerializeField] private SpriteRenderer _bottle;
    [SerializeField] private Transform _effectTransform;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private Wobble _wobble;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private ObjectType _type;

    private Transform _destination;

    private GameObject _effect;
    private Potion _potionInBottle;
    private Color _potionColor;
    private Color _contrabandBottleColor = Color.black;

    private TableManager _tableManager;
    private BottleStorage _bottleStorage;
    private LocalTimer _timer;

    private Tween _tween;   
    private bool _isFull;

    private int _contrabandTime;
    public bool IsFull => _isFull;
    public Potion PotionInBottle => _potionInBottle;

    public ObjectType Type => _type;

    private void Start()
    {        
        _tableManager = GetComponentInParent<TableManager>();
    }

    public void InitBottle(BottleStorage storage, int contrabandTime)
    {
        _potionInBottle = new Potion();       
        _bottleStorage = storage;
        _contrabandTime = contrabandTime;
    }

    public void Movement()
    {
        GetTable();
        _tween = transform.DOMove(_destination.position, moveSpeed, false).OnStart(SortInFullTable).OnComplete(ReturnBottleToBox);             
    }

    public void MovementFromGarbage()   //без повторной ативации OnComplete, спорное решение
    {
        GetTable();
        _tween = transform.DOMove(_destination.position, moveSpeed, false).OnStart(SortInFullTable);
    }

    /// <summary>
    /// Заполняет объект Potion
    /// </summary>
    /// <param name="potion"></param>
    public void FillPotionInBottle(Potion potion,Color color, ObjectType effectType)
    {
        _potionInBottle = potion;
        _isFull = true;

        FillWaterInBottle(color);
        AddEffect(ObjectPool.SharedInstance.GetObject(effectType));     
        CheckContraband();

        print("From Bottle" + _potionInBottle.PotionName);
    }

    /// <summary>
    /// Заполняет только цвет
    /// </summary>
    /// <param name="color"></param>
    private void FillWaterInBottle(Color color)
    {
        _potionColor = color;
        _fullBottle.enabled = true;
        _wobble.ChangeColor(_potionColor);
    }

    private void AddEffect(GameObject effect)
    {
        if (_potionInBottle.Rarity == ResourceRarity.rare)
        {
            _effect = effect;
            _effect.transform.position = transform.position;
            _effect.transform.SetParent(transform);
            _effect.transform.localScale = new Vector3(1, 1, 0);

            _effect.GetComponentInChildren<Effect>().ChangeParticleColor(_potionColor);
        }
    }

    private void CheckContraband()
    {
        if (_potionInBottle.Contraband)      
        {
            _bottle.color = _contrabandBottleColor;
            _timerText.gameObject.SetActive(true);
            StartTimer();
        }
        else
        {
            _bottle.color = Color.white; 
            _timerText.gameObject.SetActive(false);
        }
    }

    public void GetTable()
    {        
        if (_isFull)
            _destination = _tableManager.FullPotionTable.transform;
        else
            _destination = _tableManager.EmptyPotionTable.transform;
    }

    private void SortInFullTable()
    {
        _tableManager.FullPotionTable.SortBottlePosition();
        transform.SetParent(_destination);              
    }

    private void ReturnBottleToBox()
    {       
        _collider.enabled = true;

        if (!IsFull)
        {
            _bottleStorage.IncreaseAmount();
            DestroyBottle();
        }
    }

    public void DestroyBottle()
    {
        _tween.Pause();
        _tween.Kill(true);

        ObjectPool.SharedInstance.DestroyObject(gameObject);

        ReturnEffect();
    }

    public void ResetBottle()
    {
        ReturnEffect();

        _fullBottle.enabled = false;
        _isFull = false;
        _collider.enabled = false;

        _tween.Restart();
    }

    public void ReturnEffect()
    {
        if(_effect != null)
        {
            ObjectPool.SharedInstance.DestroyObject(_effect);
        }

        if (_timer != null)
        {
            StopCoroutine(_timer.Timer());
            _timer.OnTimerUpdate -= UpdateTimer;
        }
    }

    public void Action()
    {
       
    }

    private void StartTimer()
    {
        _timer = new LocalTimer(_contrabandTime, true);
        StartCoroutine(_timer.Timer());

        _timer.OnTimerUpdate += UpdateTimer;
    } 

    private void UpdateTimer()
    {
        _timerText.text = _timer.CurrentTime.ToString();

        if (_timer.CurrentTime < 1)
        {
            _timer.StoppedTimer();
            Money.OnDecreaseMoney?.Invoke(3000);
            Destroy(this.gameObject);
        }
    }

}

