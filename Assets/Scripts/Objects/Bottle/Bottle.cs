using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour,IAction,IPooledObject
{
    public event Action OnDropped;
    private const float moveSpeed = 0.5f;
    private Vector3 standartScale = new Vector3(0.3f, 0.3f, 1);
    private Vector3 increaseScale = new Vector3(0.4f, 0.4f, 1);
    private Vector3 tradeScale = new Vector3(0.2f, 0.2f, 1);

    [SerializeField] private BottleView _bottleView;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private LabelPhysController _labelController;
    [SerializeField] private ObjectType _type;
    [SerializeField] private ISlot _slot;
    [SerializeField] private ISlot _prevSlot;
    [SerializeField] private List<PotionLabelType> _labels;

    private Transform _destination;
    
    private Potion _potionInBottle;
    private TableManager _tableManager;
    private BottleStorage _bottleStorage;
    private BottleInventory _bottleInventory;

    private bool _isFull;

    public BottleView View => _bottleView;
    public List<PotionLabelType> Labels => _labels;
    public bool IsFull => _isFull;
    public Potion PotionInBottle => _potionInBottle;
    public ObjectType Type => _type;

    public void InitBottle(BottleStorage storage,TableManager tableManager, BottleInventory inventory)
    {
        _bottleStorage = storage;
        _tableManager = tableManager;
        _bottleInventory = inventory;
    }


    public void FillBottle(Potion potion, Color color)
    {
        if (!_isFull)
        {
            potion.Labels.Sort();
            _potionInBottle = new Potion(potion.Labels);

            SetWaterColor(color);
            _isFull = true;                       
            _bottleView.AddLabels(_bottleStorage.LabelToSprite, potion.Labels);

            _labels = new List<PotionLabelType>();
            _labels.AddRange(potion.Labels);
        }

        DOVirtual.DelayedCall(0.2f, Drop);
    }

    private void SetWaterColor(Color color)
    {
        _bottleView.FillColorWater(color);
    }

    public void SetPosition(Transform slotTransform)
    {
        slotTransform.TryGetComponent(out ISlot slot);
        if(slot != null)
        {          
            _slot = slot;
        }       

        _destination = slotTransform;
        transform.SetParent(slotTransform);

        StartCoroutine(CheckDelay());
    }

    public void Drop()
    {
        StandartSize();
        _labelController.Deactivate();

        GetTable();
        transform.DOMove(_destination.position, moveSpeed, false).SetEase(Ease.Linear)
                                                                 .OnComplete(ReturnBottleToSlot);                                                                     
        OnDropped?.Invoke();
        _slot.SetSlot(this);
    }

    public void DropFromGarbage()   //без повторной ативации OnComplete, спорное решение
    {
        GetTable();
        transform.DOMove(_destination.position, moveSpeed, false);
    }

    public void ReturnToSlot()
    {
        if(_prevSlot is FullBottleSlot slot)
        {
            SetPosition(slot.transform);
        }
        else
        {
            SetPosition(_bottleInventory.GetSlot(PotionInBottle).transform);
        }

        DOVirtual.DelayedCall(0.1f, Drop);
    }

    private void GetTable()
    {        
        if(_tableManager != null)
        {
            if (!_isFull)
                _destination = _tableManager.EmptyPotionTable.transform;         
        }
        else
        {
            Debug.Log("Стол не указан");
        }  
    }

    private void ReturnBottleToSlot()
    {
        _collider.enabled = true;
    }

    public void DestroyBottle()
    {
        DOTween.Kill(true);
        ResetBottle();
        ObjectPool.SharedInstance.DestroyObject(gameObject);       
    }

    public void ResetBottle()
    {
        _bottleView.ResetView();
        _isFull = false;
        _labels.Clear();
        _potionInBottle = null;
    }

    public void Action()
    {
        IncreaseSize();
        _labelController.Activate();

        StartCoroutine(CheckDelay());

    }

    private IEnumerator CheckDelay()
    {
        yield return new WaitForSeconds(0.2f);

        if (_slot is FullBottleSlot slot)
        {
            slot.CheckSlot();            
        }
        else if(_slot is TradeSlot tradeSlot)
        {
            tradeSlot.SetSlotFree();
        }      
    }

    public void SetTradeScale()
    {
        transform.DOScale(tradeScale, 0.5f);
    }

    public void IncreaseSize()
    {
        transform.DOScale(increaseScale, 0.5f);
    }

    public void StandartSize()
    {
        transform.DOScale(standartScale, 0.5f);
    }

    public void HideBottle(bool flag)
    {
        gameObject.SetActive(!flag);
    }

    private void OnDisable()
    {
        ResetBottle();
    }
}

