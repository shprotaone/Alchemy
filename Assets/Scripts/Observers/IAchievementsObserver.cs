using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAchievementsObserver : IObserver
{
    void Notify(AchievementData data);
}
