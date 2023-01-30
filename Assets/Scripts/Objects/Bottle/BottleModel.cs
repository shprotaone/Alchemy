using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class BottleModel : MonoBehaviour,IAction,IPooledObject,IInterract
{
    private const float moveSpeed = 0.3f;
    
    [SerializeField] private BottleView _bottleView;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private Rigidbody2D _rigid;
    [SerializeField] private LabelPhysController _labelController;
    [SerializeField] private ObjectType _type;
    [SerializeField] private BottleData _bottleData;

    public ISlot _slot;
    public ISlot _prevSlot;

    private Transform _destination; 
    private BottleStorage _bottleStorage;
    private BottleInventory _bottleInventory;
    public BottleData Data => _bottleData;
    public BottleView View => _bottleView;    
    public ObjectType Type => _type;

    public void InitBottle(BottleStorage storage, BottleInventory inventory)
    {
        _bottleStorage = storage;
        _bottleInventory = inventory;
        _bottleData = null;
    }

    public void FillBottle(Potion potion, Color color)
    {
        _collider.enabled = false;

        if (_bottleData == null)
        {
            _bottleData = new BottleData(potion, true);

            potion.Labels.Sort();
            SetWaterColor(color);
        }
        else
        {
            Debug.Log("Уже заполнена");
        }
       
        _bottleView.AddLabels(_bottleStorage.LabelToSprite, _bottleData.Labels);
        
        DOVirtual.DelayedCall(0.2f,RepositionToSlot);
    }

    private void SetWaterColor(Color color)
    {
        _bottleView.FillColorWater(color);
    }
    /// <summary>
    /// Репозиция в слот из котла
    /// </summary>
    private void RepositionToSlot()
    {       
        _bottleView.StandartSize();
        _labelController.Deactivate();

        if (_destination != null)
        {
            transform.DOMove(_destination.position, 1, false).SetEase(Ease.InOutBack, 0.8f)
                                                                 .OnComplete(ReturnBottleToSlot);
        }

        //_slot.SetSlot(this,false);        
    }

    public void SetDestination(Transform destinationTransform)
    {
        _destination = destinationTransform;
    }

    public void SetSlot(Transform slotTransform)
    {
        slotTransform.TryGetComponent(out ISlot slot);         

        if (slot != null)
        {
            _prevSlot = _slot;
            _slot = slot;
            _destination = slotTransform;
            transform.SetParent(slotTransform);
        }            
    }

    public void Drop()
    {
        //_collider.enabled = false;

        _bottleView.StandartSize();
        _labelController.Deactivate();       

        if(_destination != null)
        {
            transform.DOMove(_destination.position, moveSpeed, false).SetEase(Ease.Linear)
                                                                 .OnComplete(ReturnBottleToSlot);
        }

        _prevSlot.CheckSlot();
    }

    
    public void ReturnToSlot()
    {

        SetSlot(_bottleInventory.GetSlot(_bottleData.PotionInBottle).transform);

        Drop();
    }

    /// <summary>
    /// Действия после того, как бутылка прилетела в слот
    /// </summary>
    private void ReturnBottleToSlot()
    {
        _collider.enabled = true;
    }

    public void Action()
    {
        _bottleView.IncreaseSize();
        _labelController.Activate();      
    }

    public void DestroyBottle()
    {
        _bottleView.ResetView();
        _bottleData = null;
        _prevSlot = null;
        _slot = null;
        ObjectPool.SharedInstance.DestroyObject(gameObject);
    }

    public void SetInterract(bool value)
    {
        _collider.enabled = value;
    }
}

