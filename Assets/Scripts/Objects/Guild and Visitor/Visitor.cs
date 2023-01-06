﻿using System;
using UnityEngine;

public class Visitor : MonoBehaviour
{
    [SerializeField] private VisitorView _visitorView;
    [SerializeField] private GuildsType _currentGuild;    
    [SerializeField] private PotionTaskView _currentTaskView;

    private PotionTask _task;
    private VisitorController _visitorController;
    private LocalTimer _timer;

    private bool _firstTask = true;
    private int _timeVisitor;

    public VisitorView VisitorView => _visitorView;
    public GuildsType Guild => _currentGuild;
    public PotionTaskView TaskView => _currentTaskView;

    public void Init(VisitorController visitorController, PotionTask task)
    {      
        _visitorController = visitorController;
        _timeVisitor = _visitorController.VisitorTime;
        _task = task;

        //TimerActivate();

        _visitorView.Rising();
    } 

    private void TimerActivate()
    {
        _visitorView.UpdateTimerText(_timeVisitor);
        _timer = new LocalTimer(_timeVisitor, true);
        StartCoroutine(_timer.StartTimer());
        _timer.OnTimerUpdate += () => _visitorView.UpdateTimerText(_timer.CurrentTime);
        _timer.OnTimerEnded += CheckVisitorTime;
    }

    public bool ChekResult()
    {
        _visitorView.Fading();
        _currentTaskView.FadingTask();
        //if (_task.CurrentPotion.PotionName == task.PotionName)
        //{
        //    _task.TaskSystem.TaskComplete();
        //    _visitorView.Fading();
        //    _task.CurrentTaskView.FadingTask();
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}

        return true;
    }

    public void CheckVisitorTime(bool flag)
    {
        if (flag)
        {
            _visitorView.Fading();
            _currentTaskView.FadingTask();
            _task.TaskSystem.TaskCanceled();
        }
    }

    private void OnDisable()
    {
        _task = null;
    }
}
