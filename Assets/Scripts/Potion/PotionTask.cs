using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PotionTask
{
    private PotionTaskView _potionTaskView;
    private RewardCalculator _rewardCalculator;
    private Potion _currentPotion;
    private Visitor _visitor;
    private int _rewardCoin;
    private float _rewardRep;
    private float _penaltyRep;

    public Potion CurrentPotion => _currentPotion;
    public int RewardCoin => _rewardCoin;
    public float RewardRep => _rewardRep;
    public float PenaltyRep => _penaltyRep;
    public Visitor Visitor => _visitor;

    public PotionTask(PotionTask task)
    {
        _currentPotion = task.CurrentPotion;
        _rewardCoin = task.RewardCoin;
        _rewardRep = task.RewardRep;
        _penaltyRep = task._penaltyRep;
        _potionTaskView = task._potionTaskView;
    }
    public PotionTask(Potion potion,PotionTaskView taskView,Visitor visitor)
    {
        _currentPotion = potion;
        _visitor = visitor;
        _potionTaskView = taskView;
        SetGuild();
        SetRewardAndPenalty();
    }   

    public void SetReward(int reward)
    {
        _rewardCoin = reward;
    }
    
    public void SetGuild()
    {
        _currentPotion.SetGuild(_visitor.Guild);
    }

    public bool ChekResult(Potion potion)
    {
        if (_currentPotion.PotionName == potion.PotionName)
        {
            _potionTaskView.TaskComplete(true);
            return true;
        }
        else
        {
            _potionTaskView.TaskComplete(false);
            return false;
        }
    }

    private void SetRewardAndPenalty()
    {
        GuildRepCalculator calculator = new GuildRepCalculator();
        _rewardCalculator = new RewardCalculator();

        _rewardCoin = GetReward(_currentPotion);
        _rewardRep = calculator.CalculateReward(_currentPotion.Rarity);
        _penaltyRep = calculator.CalculatePenalty(_currentPotion.Rarity);
    }

    private int GetReward(Potion potion)
    {
        _rewardCalculator.Calculate(_visitor.Guild, potion.Rarity);     

        return (int)_rewardCalculator.Reward;
    }
}
