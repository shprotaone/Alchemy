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

    public ISlot _slot;
    public ISlot _prevSlot;

    private Transform _destination;
    [SerializeField] private BottleData _bottleData;
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

        DOVirtual.DelayedCall(0.2f, RepositionToSlot);
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
        _slot.SetSlot(this);
        _bottleView.StandartSize();
        _labelController.Deactivate();
        

        if (_destination != null)
        {
            transform.DOMove(_destination.position, 2, false).SetEase(Ease.InOutBack, 0.8f)
                                                                 .OnComplete(ReturnBottleToSlot);
        }       
    }

    public void SetPosition(Transform slotTransform)
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
        _bottleView.StandartSize();
        _labelController.Deactivate();

        if (_destination != null)
        {
            transform.DOMove(_destination.position, moveSpeed, false).SetEase(Ease.Linear)
                                                                     .OnComplete(ReturnBottleToSlot);
                                                                 
        }
        SlotBehaviourAfterDrop();
        _slot?.CheckSlot();
    }

    private void SlotBehaviourAfterDrop()
    {
        if(_slot is FullBottleSlot fullSlot)
        {
            fullSlot.SetSlot(this,true);
            fullSlot.ScaleBottles();
        }

        _prevSlot?.CheckSlot();
    }

    public void ReturnToSlot()
    {
        if (_prevSlot is FullBottleSlot slot)
        {
            SetPosition(slot.transform);
        }
        else
        {
            SetPosition(_bottleInventory.GetSlot(_bottleData.PotionInBottle).transform);
        }

        Drop();
    }

    /// <summary>
    /// Действия после того, как бутылка прилетела в слот
    /// </summary>
    private void ReturnBottleToSlot()
    {
        _collider.enabled = true;
        if (_slot is FullBottleSlot bottleSlot) bottleSlot.ScaleBottles();
    }

    public void Action()
    {
        _bottleView.IncreaseSize();
        _labelController.Activate();

        if (_slot is FullBottleSlot fullSlot)
        {
            fullSlot.DeleteBottle(this);
            fullSlot.CheckCountSlot();
        }

        _prevSlot?.CheckSlot();
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

    public void SetForcePosition(Vector3 position)
    {
        this.transform.position = position;        
    }
}

