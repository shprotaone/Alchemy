using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bottle : MonoBehaviour,IAction,IPooledObject
{
    private const float moveSpeed = 1;

    [SerializeField] private BottleView _bottleView;
    [SerializeField] private BoxCollider2D _collider;  
    [SerializeField] private ObjectType _type;
    [SerializeField] private string _namePotionInBottle;

    private Transform _destination;
    
    private Potion _potionInBottle;
    private TableManager _tableManager;
    private BottleStorage _bottleStorage;

    private bool _isFull;

    public List<PotionLabelType> LabelList { get; private set; }
    private int _contrabandTime;
    public bool IsFull => _isFull;
    public Potion PotionInBottle => _potionInBottle;
    public ObjectType Type => _type;

    public void InitBottle(BottleStorage storage,TableManager tableManager)
    {             
        _bottleStorage = storage;
        _contrabandTime = 10;
        _tableManager = tableManager;

        LabelList = new List<PotionLabelType>();
    }

    public void SetLabel(List<PotionLabelType> label)
    {
        _bottleView.AddLabels(_bottleStorage.LabelToSprite,label);
        _isFull = true;
    }

    public void SetWaterColor(Color color)
    {
        _bottleView.FillColorWater(color);
    }
    public void SetPotion(Potion potion)
    {
        if (!_isFull)
        {
            _potionInBottle = new Potion(potion);
            _isFull = true;            
            _bottleView.AddEffect(potion);                    

            print("From Bottle" + _potionInBottle.PotionName);
            //CheckContraband();
            //_namePotionInBottle = _potionInBottle.PotionName;
        }
        else
        {
            Debug.LogWarning("Бутылка уже заполнена");
        }
    }
    public void SetPosition(Transform slotTransform)
    {
        _destination = slotTransform;
        transform.SetParent(slotTransform);
    }

    public void Drop()
    {
        GetTable();
        transform.DOMove(_destination.position, moveSpeed, false).OnComplete(ReturnBottleToBox);             
    }

    public void DropFromGarbage()   //без повторной ативации OnComplete, спорное решение
    {
        GetTable();
        transform.DOMove(_destination.position, moveSpeed, false);
    }  

    private void CheckContraband()
    {
        //if (_potionInBottle.Contraband)
        //{
        //    _bottle.color = _contrabandBottleColor;
        //    _timerText.gameObject.SetActive(true);
        //    //StartTimer();                         //вынести контрабадные зелья и наполнения в отдельные классы? 
        //}
        //else
        //{
        //    _bottle.color = Color.white;
        //    _timerText.gameObject.SetActive(false);
        //}
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

    public void Action()
    {
       
    }
}

