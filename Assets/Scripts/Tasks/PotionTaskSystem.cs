using System;
using System.Collections.Generic;
using UnityEngine;

public class PotionTaskSystem : MonoBehaviour
{
    [SerializeField] private DayNotifySubject _dayNotifySubject;
    [SerializeField] private TradeSystem _tradeSystem;
    [SerializeField] private VisitorController _visitorController;       
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Transform _jarTransform;

    private TaskLabelCountChance _chanceLabelCount;
    private LabelToSprite _labelToSprite;
    private RewardCalculator _rewardCalculator;
    private List<PotionLabelType> _labels;   
    private Potion _currentPotion;

    public void Init(List<RandomPart> countChances,List<RandomPart> labelChances, LabelToSprite labelToSprite, bool withNotify)
    {
        _currentPotion = new Potion();
        _rewardCalculator = new RewardCalculator();
        _labelToSprite = labelToSprite;
        _chanceLabelCount = new TaskLabelCountChance(countChances,labelChances);

        Notify(withNotify);
    }

    private void Notify(bool isActive)
    {
        _dayNotifySubject.SetNotify(_chanceLabelCount.GetLabelWithMaxWeight(),isActive);
    }

    public PotionTask GetTaskv2()
    {
        PotionTask potionTask;

        _currentPotion = GetPotionForTask();

        potionTask = new PotionTask(_currentPotion, _visitorController, this);
        potionTask.SetReward((int)_rewardCalculator.GetReward(_currentPotion.Labels.Count));

        _tradeSystem.SetTask(potionTask);

        return potionTask;
    }    

    private Potion GetPotionForTask()
    {
        int count = _chanceLabelCount.GetLabelIndex();

        _labels = new List<PotionLabelType>();    

        for (int i = 0; i < count; i++)
        {
            _labels.Add(_chanceLabelCount.GetLabel());
        }

        return new Potion(_labels);
    }  

    public Sprite[] GetLabels(int count)
    {
        Sprite[] sprites = new Sprite[count];

        for (int i = 0; i < count; i++)
        {
            sprites[i] = _labelToSprite.GetSprite(_labels[i],false);
        }

        return sprites;
    }
}