using System;
using System.Collections;
using UnityEngine;

public class Visitor : MonoBehaviour
{
    public event Action<bool> OnVisitorSleep;
    [SerializeField] private VisitorView _visitorView;
    [SerializeField] private GuildsType _currentGuildType;    
    [SerializeField] private PotionTaskView _currentTaskView;

    public VisitorView VisitorView => _visitorView;
    public PotionTaskView TaskView => _currentTaskView;
    public GuildsType GuildsType => _currentGuildType;

    public void Init()
    {
        _visitorView.Rising();
        StartCoroutine(SleepRoutine());
    }

    public IEnumerator SleepRoutine()
    {
        int timeToSleep = 15;

        while (timeToSleep > 0)
        {
            timeToSleep--;
            yield return new WaitForSeconds(1);
        }

        OnVisitorSleep?.Invoke(true);

        yield return new WaitForSeconds(3);

        OnVisitorSleep?.Invoke(false);
    }

    public void ShowEmoji()
    {
        TaskView.EmojiController.ShowEmoji();
    }

    public void Disable()
    {
        OnVisitorSleep?.Invoke(false);
        _visitorView.Fading();
        _currentTaskView.Fading();
    }

    public void SetEmoji(float index)
    {
        TaskView.EmojiController.SetEmoji(index);
    }
}
