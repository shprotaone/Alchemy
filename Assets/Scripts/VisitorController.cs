﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VisitorController : MonoBehaviour
{
    private const float taskTime = 60;

    [SerializeField] private Visitor[] _visitors;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private bool _shopIsOpen = false;

    private Visitor _currentVisitor;
    private Visitor _prevVisitor;
    
    private float _currentTimer;

    public bool ShopIsOpen 
    { 
        get { return _shopIsOpen; }
        set 
        { 
            _shopIsOpen = value;
            if (_shopIsOpen)
                CallVisitor();
            else
                DisableVisitor();
        }    
    }
    private void Start()
    {
        if(_shopIsOpen)
        CallVisitor();
    }

    public void CallVisitor()
    {
        if (_shopIsOpen)
        {
            _currentVisitor = _visitors[Random.Range(0, _visitors.Length)];

            if (_currentVisitor == _prevVisitor)
            {
                _currentVisitor = _visitors[Random.Range(0, _visitors.Length)];
            }
            _currentVisitor.gameObject.SetActive(true);
            _currentVisitor.Rising();

            _prevVisitor = _currentVisitor;
        }        
    }

    public void DisableVisitor()
    {
        _currentVisitor.Fading();
    }

    public void UpdateTimerDisplay(float value)
    {
        float seconds = Mathf.FloorToInt(value % 60);
        _timerText.text = seconds.ToString();
    }
}
