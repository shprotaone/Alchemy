using System;
using UnityEngine;

[CreateAssetMenu]
public class AchievementData : ScriptableObject
{
    public event Action<AchievementData> OnUpdate;

    public string Name;
    public string Desription;
    public AchieveID Id;
    public int Goal;
    public int GoalProgress;
    public bool Complete;

    public void Update()
    {
        OnUpdate?.Invoke(this);
    }

    public void Increase()
    {
        GoalProgress++;
        CheckGoal();
        
    }

    public void IncreaseWithCount(int count)
    {
        GoalProgress += count;
        CheckGoal();
    }

    private void CheckGoal()
    {
        if (Goal <= GoalProgress)
        {
            Complete = true;
        }
        OnUpdate?.Invoke(this);
    }
}
