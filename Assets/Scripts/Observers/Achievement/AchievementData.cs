using System;
using UnityEngine;

[CreateAssetMenu]
public class AchievementData : ScriptableObject
{
    public event Action<AchievementData> OnUpdate;

    public string Name;
    public AchieveID ID;
    public int goal;
    public int goalProgress;
    public bool complete;

    public void Update()
    {
        OnUpdate?.Invoke(this);
    }

    public void Increase()
    {
        goalProgress++;

        if (goal <= goalProgress)
        {
            complete = true;
        }
        OnUpdate?.Invoke(this);        
    }

}
