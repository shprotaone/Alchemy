using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Bottle : MonoBehaviour,IAction,IPooledObject
{
    public event Action OnDropped;
    private const float moveSpeed = 1;
    private Vector3 standartScale = new Vector3(0.3f, 0.3f, 1);
    private Vector3 increaseScale = new Vector3(0.4f, 0.4f, 1);
    private Vector3 tradeScale = new Vector3(0.2f, 0.2f, 1);

    [SerializeField] private BottleView _bottleView;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private LabelPhysController _labelController;
    [SerializeField] private ObjectType _type;
    [SerializeField] private FullBottleSlot _slot;
    [SerializeField] private FullBottleSlot _prevSlot;
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

    public void InitBottle(BottleStorage storage,TableManager tableManager)
    {
        _bottleStorage = storage;
        _tableManager = tableManager;
    }


    public void FillBottle(Potion potion, Color color)
    {
        if (!_isFull)
        {
            _potionInBottle = new Potion(potion.Labels);

            SetWaterColor(color);
            _isFull = true;                       
            _bottleView.AddLabels(_bottleStorage.LabelToSprite, potion.Labels);

            _labels = new List<PotionLabelType>();
            Labels.AddRange(potion.Labels);
        }
        else
        {
            Debug.LogWarning("Бутылка уже заполнена");
        }
    }

    private void SetWaterColor(Color color)
    {
        _bottleView.FillColorWater(color);
    }

    public void SetPosition(Transform slotTransform)
    {
        slotTransform.TryGetComponent(out FullBottleSlot slot);
        if(slot != null)
        {          
            _slot = slot;
        }       

        _destination = slotTransform;
        transform.SetParent(slotTransform);

        _slot.CheckChild();
    }

    public void Drop()
    {
        DecreaseSize();
        _labelController.Deactivate();

        GetTable();
        transform.DOMove(_destination.position, moveSpeed, false).SetEase(Ease.Linear)
                                                                 .OnComplete(ReturnBottleToBox);
        OnDropped?.Invoke();
    }

    public void DropFromGarbage()   //без повторной ативации OnComplete, спорное решение
    {
        GetTable();
        transform.DOMove(_destination.position, moveSpeed, false);
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

    private void ReturnBottleToBox()
    {       
        _collider.enabled = true;
        
        if (!IsFull)
        {
            _bottleStorage.ReturnBottle();
            DestroyBottle();
        }
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
        _collider.enabled = false;
    }

    public async void Action()
    {
        IncreaseSize();
        _labelController.Activate();
        _prevSlot = _slot;
        await Task.Delay(50);
        _prevSlot.CheckChild();
    }

    public void SetTradeScale()
    {
        transform.DOScale(tradeScale, 0.5f);
    }

    private void IncreaseSize()
    {
        transform.DOScale(increaseScale, 0.5f);
    }

    private void DecreaseSize()
    {
        transform.DOScale(standartScale, 0.5f);
    }
}

