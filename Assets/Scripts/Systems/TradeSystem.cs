using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TradeSystem : MonoBehaviour
{
    [SerializeField] private List<TradeSlot> _slots;
    [SerializeField] private List<PotionLabelType> _labelsIn;
    [SerializeField] private List<PotionLabelType> _labelInTask;
    [SerializeField] private TradeSystemView _tradeView;
    [SerializeField] private LibraVisual _libraVisual;
    
    private VisitorController _visitorController;  
    private Money _money;
    private RewardCalculator _rewardCalculator;
    private MatchCalculate _matchCalculate;
    private CompleteSeries _completeSeries;
    private PotionTask _task;

    private int _reward;
    private int _indexMatch;

    public void Init(VisitorController visitorController,Money money) 
    {
        _tradeView.Init(this);
        _visitorController = visitorController;

        _rewardCalculator = new RewardCalculator();
        _matchCalculate = new MatchCalculate();
        _labelsIn = new List<PotionLabelType>();

        _completeSeries = new CompleteSeries();
        _tradeView.RefreshMultiply(_completeSeries.CurrentMultiply);

        _money = money;              
    }

    public void SetTask(PotionTask task)
    {
        _task = task;
        _labelInTask = _task.CurrentPotion.Labels;
    }

    public void FillLabels(List<PotionLabelType> label)
    {
        _labelsIn.AddRange(label);
        _libraVisual.CheckPosition(label.Count);
        CalculateReward();
    }

    public void ClearLabelList()
    {
        _labelsIn.Clear();
    }

    public void DeleteLabel(List<PotionLabelType> labels)
    {
        foreach (var item in labels)
        {
            _labelsIn.Remove(item);
        }

        CalculateReward();       
    }

    public void CalculateReward()
    {
        _indexMatch = _matchCalculate.IndexMatchLabel(_labelsIn, _task.CurrentPotion.Labels);
        float result = _rewardCalculator.GetReward(_indexMatch);

        _reward = (int)(result * _completeSeries.CurrentMultiply);
        _tradeView.Refresh(_reward);
        _tradeView.TradeButtoneControl(_reward);
        _libraVisual.CheckPosition(_labelsIn.Count);
    }

    public void Trade()
    {
        ClearLabelList();

        _money.Increase(_reward);
        _visitorController.DisableVisitor();
        DOVirtual.DelayedCall(0.5f, _visitorController.CallVisitor);
        _completeSeries.IncreaseMultiply(_indexMatch);
        _tradeView.RefreshMultiply(_completeSeries.CurrentMultiply);
        _tradeView.TradeButtoneControl(0);

        ClearSlots(false);
    }

    public void DeclineTrade()
    {
        ClearLabelList();
        _money.Decrease(100);
        _visitorController.DisableVisitor();
        DOVirtual.DelayedCall(0.5f, _visitorController.CallVisitor);
        
        _completeSeries.ResetSeries();
        _tradeView.RefreshMultiply(_completeSeries.CurrentMultiply);

        ReturnBottle();
        ClearSlots(true);
               
    }

    private void ClearSlots(bool decline)
    {
        if (decline)
        {
            foreach (var slot in _slots)
            {
                if (!slot.IsFree)
                {
                    slot.SetSlotFree();
                }
            }
        }
        else
        {
            foreach (var slot in _slots)
            {
                if (!slot.IsFree)
                {
                    slot.ResetSlot();
                }
            }
        }

        CalculateReward();
        _tradeView.Refresh(0);
        _libraVisual.ResetPos();
    }

    private void ReturnBottle()
    {
        foreach (var slot in _slots)
        {
            if (!slot.IsFree)
            {
                slot.BottleInSlot.ReturnToSlot();                
            }
        }

        //DOVirtual.DelayedCall(1f, ClearSlots);
    }

    public void Disable()
    {
        _tradeView.Disable();
        _task = null;
        _labelInTask.Clear();
    }

   

    private void StartCoinAnimation()
    {
        //GameObject curCoin = ObjectPool.SharedInstance.GetObject(ObjectType.COINDROP);
        //curCoin.transform.position = _currentTask.CurrentTaskView.transform.position;
        //Coin coin = curCoin.GetComponent<Coin>();
        //coin.Movement(_jarTransform.position);
    }
}
