using UnityEngine;

public class PotionTask
{
    private RewardCalculator _rewardCalculator;
    private Potion _currentPotion;
    private int _rewardCoin;

    public PotionTaskView CurrentTaskView { get; private set; }
    public PotionTaskSystem TaskSystem { get; private set; }
    public Sprite[] Images { get; private set; }
    public Potion CurrentPotion => _currentPotion;
    public int RewardCoin => _rewardCoin;

    public PotionTask(Potion potion, VisitorController visitorController, PotionTaskSystem taskSystem)
    {
        TaskSystem = taskSystem;
        _currentPotion = potion;

        SetGuild(potion.GuildsType);
        SetReward();
       
        Images = taskSystem.GetIngredientSprites(this);

        CurrentTaskView = visitorController.CurrentVisitor.TaskView;
        CurrentTaskView.InitTask(this, taskSystem.ImageTask);
    }   
    
    private void SetGuild(GuildsType guildType)
    {
        _currentPotion.SetGuild(guildType);
    }

    private void SetReward()
    {
        _rewardCalculator = new RewardCalculator();
        _rewardCalculator.CalculateBase(_currentPotion.GuildsType, _currentPotion.Rarity);
        _rewardCoin = (int)_rewardCalculator.Reward;
    }
}
