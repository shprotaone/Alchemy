using System;
using UnityEngine;
using DG.Tweening;

public class Visitor : MonoBehaviour
{
    [SerializeField] private VisitorView _visitorView;
    [SerializeField] private GuildsType _currentGuild;    
    [SerializeField] private PotionTaskView _currentTaskView;

    private PotionTask _task;

    public VisitorView VisitorView => _visitorView;
    public PotionTaskView TaskView => _currentTaskView;

    public void Init(PotionTask task)
    {      
        _task = task;

        _visitorView.Rising();
    }

    public void Disable()
    {
        _visitorView.Fading();
        _currentTaskView.FadingTask();
    }

    private void OnDisable()
    {
        _task = null;
        _visitorView.RefreshView();
    }
}
