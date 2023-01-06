using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradeSystem : MonoBehaviour
{
    [SerializeField] private List<TradeSlot> _slots;
    [SerializeField] private List<PotionLabelType> _labels;
    [SerializeField] private TradeSystemView _tradeView;
    
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
        _labels = new List<PotionLabelType>();

        _completeSeries = new CompleteSeries();
        _tradeView.RefreshMultiply(_completeSeries.CurrentMultiply);

        _money = money;              
    }

    public void SetTask(PotionTask task)
    {
        _task = task;
    }

    public void FillLabels(List<PotionLabelType> label)
    {
        if (_labels.Count < 3)
        {
            _labels.AddRange(label);
            CalculateReward();
        }
        else
        {
            Debug.Log("Переполнение");
        }
         
    }

    public void ClearLabelList()
    {
        _labels.Clear();
    }

    public void DeleteLabel(PotionLabelType label)
    {
        if(_labels.Count != 0)
        {
            _labels.Remove(label);
            CalculateReward();
        }
    }

    public void CalculateReward()
    {
        _indexMatch = _matchCalculate.IndexMatchLabel(_labels, _task.CurrentPotion.Labels);
        float result = _rewardCalculator.GetReward(_indexMatch);

        _reward = (int)(result * _completeSeries.CurrentMultiply);
        _tradeView.Refresh(_reward);
    }

    public void Trade()
    {
        ClearLabelList();

        _money.Increase(_reward);
        _visitorController.DisableVisitor();
        _completeSeries.IncreaseMultiply(_indexMatch);
        _tradeView.RefreshMultiply(_completeSeries.CurrentMultiply);

        ClearSlots();
    }

    public void DeclineTrade()
    {
        ClearLabelList();

        _visitorController.DisableVisitor();
        _money.Decrease(100);
        _completeSeries.ResetSeries();

        ClearSlots();
    }

    private void ClearSlots()
    {
        foreach (var slot in _slots)
        {
            slot.Reset();
        }

        _tradeView.Refresh(0);
    }

    private void StartCoinAnimation()
    {
        //GameObject curCoin = ObjectPool.SharedInstance.GetObject(ObjectType.COINDROP);
        //curCoin.transform.position = _currentTask.CurrentTaskView.transform.position;
        //Coin coin = curCoin.GetComponent<Coin>();
        //coin.Movement(_jarTransform.position);
    }
}
