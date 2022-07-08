using System;
using UnityEngine;

public class EventCounter : MonoBehaviour
{
    /// <summary>
    /// Общий счетчик события
    /// </summary>
    public static Action<int> OnIncreasedEventCount;

    private int[] _eventCounts;
    private int _currentEventCount;

    public int CurrentCount => _currentEventCount;
    public int[] EventCount => _eventCounts;

    private void Start()
    {
        NextCountHandler.OnNextCount += NextCount;
    }
    public void SetEventCounterArray(int[] counts)
    {
        _eventCounts = counts;
    }

    public void NextCount()     //раздвинуть counter как то
    {
        _currentEventCount++;
        OnIncreasedEventCount?.Invoke(_currentEventCount);
    }

    private void OnDisable()
    {
        NextCountHandler.OnNextCount -= NextCount;
    }
}
