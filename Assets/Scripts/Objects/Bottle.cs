﻿using DG.Tweening;
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
    private Transform _effectBaseTransform;

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
    }

    public void InitBottle(BottleStorage storage)
    {
        _potionInBottle = new Potion();       
        _bottleStorage = storage;
    }

    public void Movement()
    {
        GetTable();

        _tween = transform.DOMove(_destination.position, moveSpeed, false).OnStart(SortInFullTable).OnComplete(Return);
    }

    /// <summary>
    /// Заполняет объект Potion
    /// </summary>
    /// <param name="potion"></param>
    public void FillPotionInBottle(Potion potion,Color color, GameObject effect)
    {
        _potionInBottle = potion;
        _isFull = true;

        FillWaterInBottle(color);
        AddEffect(effect);     
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
            _effectBaseTransform = effect.transform.parent;

            _effect = effect;
            _effect.SetActive(true);

            _effect.transform.position = transform.position;
            _effect.transform.SetParent(transform);
            _effect.transform.localScale = new Vector3(1, 1, 0);

            _effect.GetComponentInChildren<EffectChangeColor>().ChangeParticleColor(_potionColor);
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

    /// <summary>
    /// Вызывается только при возвращении бутылки. 
    /// </summary>
    private void Return()
    {
        _collider.enabled = true;

        if (!_isFull)
        {
            _tween.Pause();
            _tween.Kill(true);
            Destroy(gameObject);           
        }
    }

    public void ResetBottle()
    {
        ReturnEffect();

        _fullBottle.enabled = false;
        _isFull = false;
        _collider.enabled = false;

        _tween.Restart();
    }

    private void ReturnEffect()
    {
        if(gameObject != null &&  _effect != null)
        {
            _effect.transform.SetParent(_effectBaseTransform);
            _effect.SetActive(false);
        }
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
        _bottleStorage.IncreaseAmount();
        ReturnEffect();

        if (_timer != null)
        {
            StopCoroutine(_timer.Timer());
            _timer.OnTimerUpdate -= UpdateTimer;
        }       
    }
}

