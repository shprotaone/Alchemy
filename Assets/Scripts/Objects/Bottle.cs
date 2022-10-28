using DG.Tweening;
using TMPro;
using UnityEngine;

public class Bottle : MonoBehaviour,IAction
{
    private const float moveSpeed = 1;
    private int _maxTimerValue = 40;

    [SerializeField] private SpriteRenderer _fullBottle;
    [SerializeField] private SpriteRenderer _bottle;
    [SerializeField] private Transform _effectTransform;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private Wobble _wobble;
    [SerializeField] private TMP_Text _timerText;

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
    public bool IsFull => _isFull;
    public Potion PotionInBottle => _potionInBottle;

    private void Start()
    {        
        _tableManager = GetComponentInParent<TableManager>();
        GetTable();
    }

    public void InitBottle(BottleStorage storage)
    {
        _potionInBottle = new Potion();       
        _bottleStorage = storage;
    }

    public void Movement()
    {
        GetTable();

        _tween = transform.DOMove(_destination.position, moveSpeed, false).OnStart(SortInFullTable).OnComplete(EndMove);
    }

    private void EndMove()
    {
        if (IsFull)
        {
            //SortInFullTable();
        }
        else
        {
            Return();
        }
        _collider.enabled = true;
    }

    /// <summary>
    /// Заполняет объект Potion
    /// </summary>
    /// <param name="potion"></param>
    public void FillPotionInBottle(Potion potion,Color color)
    {
        _potionInBottle = potion;
        _isFull = true;       

        if (potion.Rarity == ResourceRarity.rare)
        {
            _effect = Instantiate(potion.Effect,_effectTransform.position, Quaternion.identity);
            _effect.GetComponentInChildren<EffectChangeColor>().ChangeParticleColor(_potionColor);
            _effect.transform.SetParent(transform);
            _effect.transform.localScale = new Vector3(1, 1, 0);
        }

        FillWaterInBottle(color);
        CheckContraband(potion);

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

    private void CheckContraband(Potion potion)
    {
        if (_potionInBottle.Contraband)      //не переносятся данные
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

    private void GetTable()
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

    /// <summary>
    /// Вызывается только при возвращении бутылки. 
    /// </summary>
    private void Return()
    {
        _collider.enabled = true;

        if (!_isFull)
        {
            _tween.Kill(true);
            Destroy(gameObject);
            _bottleStorage.IncreaseAmount();
        }
    }

    public void ResetBottle()
    {
        Destroy(_effect);
        _fullBottle.enabled = false;
        _isFull = false;
        _collider.enabled = false;

        GetTable();
        _tween.Restart();
    }

    public void Action()
    {
       
    }
    private void StartTimer()
    {
        _timer = new LocalTimer(_maxTimerValue, true);
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

    private void OnDisable()
    {
        if (_timer != null)
        {
            StopCoroutine(_timer.Timer());
            _timer.OnTimerUpdate -= UpdateTimer;
        }       
    }
}

