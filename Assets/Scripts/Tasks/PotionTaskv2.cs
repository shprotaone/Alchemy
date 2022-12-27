using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionTaskv2
{
    private List<PotionLabelType> _labels;
    private int _rewardCoin;

    public PotionTaskView CurrentTaskView { get; private set; }
    public PotionTaskSystemv2 TaskSystem { get; private set; }
    public Sprite[] Images { get; private set; }
    public int RewardCoin => _rewardCoin;

    public PotionTaskv2(List<PotionLabelType> labels,int reward, Sprite[] sprites)
    {
        _labels = labels;
        _rewardCoin = reward;
        Images = sprites;
    }
}
