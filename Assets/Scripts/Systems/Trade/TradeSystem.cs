using System;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class TradeSystem : MonoBehaviour
{
    public event Action OnDecline;

    [SerializeField] private TradeSlot _tradeSlot;
    [SerializeField] private List<PotionLabelType> _labelsIn;
    [SerializeField] private List<PotionLabelType> _labelInTask;
    [SerializeField] private TradeSystemView _tradeView;
    [SerializeField] private LibraVisual _libraVisual;
    [SerializeField] private StepHandler _step5Handler;
    [SerializeField] private StepHandler _step6Handler;
    
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
        _tradeSlot.Init(this);

        _visitorController = visitorController;
        _audioManager = audioManager;

        _rewardCalculator = new RewardCalculator();
        _matchCalculate = new MatchCalculate();
        _labelsIn = new List<PotionLabelType>();

        _completeSeries = new CompleteSeries();
        _tradeView.RefreshMultiplyValue(_completeSeries.CurrentMultiply);

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
    }

    private void ClearLabelList()
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
        _audioManager.PlaySFX(_audioManager.Data.Scales);

        _step5Handler?.Call();
    }

    public void Trade()
    {
        _visitorController.FillEmojiStatus(_matchCalculate.GetEmojiIndex());
        ClearLabelList();

        _money.Increase(_reward);
        _tradeView.Refresh(0);
        _visitorController.GoAwayVisitor();
        _tradeView.DeclineButtonDelay();

        _completeSeries.IncreaseMultiply(_indexMatch);
        _tradeView.RefreshMultiplyValue(_completeSeries.CurrentMultiply);
        _tradeView.StartCoinAnimation();
        _tradeView.TradeButtoneControl(0);
        _audioManager.PlaySFX(_audioManager.Data.CoinDrop);
        _audioManager.PlaySFX(_audioManager.Data.VoiceGood);

        ClearSlotsAfterTrade();
        _step6Handler?.Call();

        DOVirtual.DelayedCall(1f,_visitorController.CallVisitor);
    }

    public void DeclineTrade()
    {
        _visitorController.FillEmojiStatus(0);
        ClearLabelList();
        _money.Decrease(100);
        _visitorController.GoAwayVisitor();
        _tradeView.DeclineButtonDelay();

        _completeSeries.ResetSeries();
        _tradeView.RefreshMultiplyValue(_completeSeries.CurrentMultiply);
        _audioManager.PlaySFX(_audioManager.Data.VoiceSad);

        ReturnBottleAfterDecline();
        OnDecline?.Invoke();

        DOVirtual.DelayedCall(1f, _visitorController.CallVisitor);
    }

    private void ClearSlotsAfterTrade()
    {
        _tradeSlot.ResetAllBottlesAfterTrade();
        _libraVisual.ResetPos();
        _tradeView.Refresh(0);
    }

    private void ReturnBottleAfterDecline()
    {
        _tradeSlot.ReturnBottles();
    }

    public void Disable()
    {
        _tradeView.Disable();
        _task = null;
        _labelInTask.Clear();
    }
}
