﻿using System;
using UnityEngine;

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

    private void OnDisable()
    {
        _task = null;
    }
}
