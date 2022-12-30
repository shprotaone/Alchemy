using UnityEngine;
using System;

public class Slot : MonoBehaviour,IAction,IInterract,IDragTimer
{
    public event Action<int> OnChangedValue;

    [SerializeField] private SlotView _slotView;
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private int _delay;
    [SerializeField] private bool _interactive;

    private LocalTimer _timer;         
    private int _amountInSlot;

    public IngredientData IngredientInSlot { get; private set; }
    public SlotView SlotView => _slotView;
    public int AmountInSlot => _amountInSlot;
    public void FillSlot(IngredientData ingredientData)
    {       
        IngredientInSlot = ingredientData;            

        _slotView.InitView(ingredientData.mainSprite);
        OnChangedValue += _slotView.RefreshAmount;
        InitTimer(_delay);
    }

    public void Action()
    {
        if (!_timer.Started && _interactive && _amountInSlot > 0)
        {
            StartTimer();
            DecreaseAmount();

            GameObject ingredientGO = ObjectPool.SharedInstance.GetObject(ObjectType.INGREDIENT);
            ingredientGO.transform.SetParent(this.transform);
            Ingredient ingredient = ingredientGO.GetComponent<Ingredient>();

            ingredient.SetIngredientData(IngredientInSlot); //??
            ingredient.SetSlot(this);
            _boxCollider.enabled = false;
        }
    }

    public void SetStartValue(int value)
    {
        _amountInSlot = value;
        OnChangedValue?.Invoke(_amountInSlot);
    }

    public void IncreaseAmount()
    {
        _amountInSlot++;
        OnChangedValue?.Invoke(_amountInSlot);
    }

    public void DecreaseAmount()
    {
        _amountInSlot--;
        OnChangedValue?.Invoke(_amountInSlot);
    } 

    public void Drop() { print("Hey"); }

    public void SetInterract(bool value)
    {
        _interactive = value;
        _boxCollider.enabled = true;
    }

    public void InitTimer(int delayDrag)
    {
        _timer = new LocalTimer(delayDrag, false);
        _timer.OnTimerEnded += SetInterract;
    }

    public void StartTimer()
    {        
        StartCoroutine(_timer.StartTimer());        
    }
}

