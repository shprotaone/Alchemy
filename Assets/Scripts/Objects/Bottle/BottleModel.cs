﻿using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class BottleModel : MonoBehaviour,IAction,IPooledObject
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
    private BottleData _bottleData;

    private int _calls = 0;

    private TableManager _tableManager;
    private BottleStorage _bottleStorage;
    private BottleInventory _bottleInventory;
    public BottleData BottleData => _bottleData;
    public BottleView View => _bottleView;    
    public ObjectType Type => _type;

    public void InitBottle(BottleStorage storage,TableManager tableManager, BottleInventory inventory)
    {
        _bottleStorage = storage;
        _tableManager = tableManager;
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
            transform.DOMove(_destination.position, 2, false).SetEase(Ease.InOutBack, 0.8f)
                                                                 .OnComplete(ReturnBottleToSlot);
        }

        _slot.SetSlot(this,false);        
    }


    public void SetPosition(Transform slotTransform)
    {
        slotTransform.TryGetComponent(out ISlot slot);

        if(slot != null)
        {
            _prevSlot = _slot;
            _slot = slot;
            _destination = slotTransform;
            transform.SetParent(slotTransform);
        }            
    }

    public void Drop()
    {
        //_rigid.simulated = false;
        _collider.enabled = false;

        _bottleView.StandartSize();
        _labelController.Deactivate();       

        if(_destination != null)
        {
            transform.DOMove(_destination.position, moveSpeed, false).SetEase(Ease.Linear)
                                                                 .OnComplete(ReturnBottleToSlot);
        }

        //SlotBehaviour(_prevSlot);
        //_slot.CheckSlot();
            
    }

    
    public void ReturnToSlot()
    {
        if(_prevSlot is FullBottleSlot slot)
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
        //_rigid.MovePosition(Vector3.zero);
        //DOVirtual.DelayedCall(1, () =>
        //{
        //    _rigid.simulated = true;
        //});
    }

    public void Action()
    {
        _bottleView.IncreaseSize();
        _labelController.Activate();

        if (_slot is FullBottleSlot fullSlot)
        {
            DOVirtual.DelayedCall(0.2f, fullSlot.CheckCountSlot);
        }
         
    }

    public void DestroyBottle()
    {
        _bottleView.ResetView();
        _bottleData = null;
        _prevSlot = null;
        _slot = null;
        ObjectPool.SharedInstance.DestroyObject(gameObject);
    }
}

