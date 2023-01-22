using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class TradeSystem : MonoBehaviour
{
    [SerializeField] private List<TradeSlot> _slots;
    [SerializeField] private List<PotionLabelType> _labelsIn;
    [SerializeField] private List<PotionLabelType> _labelInTask;
    [SerializeField] private TradeSystemView _tradeView;
    [SerializeField] private LibraVisual _libraVisual;
    
    private VisitorController _visitorController;
    private AudioManager _audioManager;
    private Money _money;
    private RewardCalculator _rewardCalculator;
    private MatchCalculate _matchCalculate;
    private CompleteSeries _completeSeries;
    private PotionTask _task;

    private int _reward;
    private int _indexMatch;

    public void Init(VisitorController visitorController,Money money,AudioManager audioManager) 
    {
        _tradeView.Init(this);
        _visitorController = visitorController;
        _audioManager = audioManager;

        _rewardCalculator = new RewardCalculator();
        _matchCalculate = new MatchCalculate();
        _labelsIn = new List<PotionLabelType>();

        _completeSeries = new CompleteSeries();
        _tradeView.RestartMultiply(_completeSeries.CurrentMultiply);

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
        _tradeView.Refresh(0);
        _visitorController.CallVisitor();
        _tradeView.DeclineButtonDelay();

        _completeSeries.IncreaseMultiply(_indexMatch);
        _tradeView.RestartMultiply(_completeSeries.CurrentMultiply);
        _tradeView.StartCoinAnimation();
        _tradeView.TradeButtoneControl(0);
        _audioManager.PlaySFX(_audioManager.Data.CoinDrop);

        ClearSlots(false);
    }

    public void DeclineTrade()
    {
        ClearLabelList();
        _money.Decrease(100);
        _visitorController.CallVisitor();
        _tradeView.DeclineButtonDelay();

        _completeSeries.ResetSeries();
        _tradeView.RestartMultiply(_completeSeries.CurrentMultiply);
        _audioManager.PlaySFX(_audioManager.GetRandomSound(_audioManager.Data.CancelClips));

        ReturnBottle();
        DOVirtual.DelayedCall(0.3f,() => ClearSlots(true));
               
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
    }

    public void Disable()
    {
        _tradeView.Disable();
        _task = null;
        _labelInTask.Clear();
    }
}
