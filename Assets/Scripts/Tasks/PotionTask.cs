using UnityEngine;

public class PotionTask
{
    private Potion _currentPotion;
    private int _rewardCoin;

    public PotionTaskView CurrentTaskView { get; private set; }
    public PotionTaskSystem TaskSystem { get; private set; }
    public Sprite[] Images { get; private set; }
    public Potion CurrentPotion => _currentPotion;
    public int RewardCoin => _rewardCoin;

    public PotionTask(Potion potion, VisitorController visitorController, PotionTaskSystem taskSystem)
    {
        Visitor visitor = visitorController.CurrentVisitor;

        TaskSystem = taskSystem;
        _currentPotion = potion;
       
        Images = taskSystem.GetLabels(potion.Labels.Count);

        CurrentTaskView = visitor.TaskView;
        CurrentTaskView.InitTask(this,visitor);
    }   

    public void SetReward(int reward)
    {
        _rewardCoin = reward;        
    }
}
