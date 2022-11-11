using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using LimitedNumbers;
using UnityEngine;
using static ButtonMode;

public sealed class ClickController : MonoBehaviour
{
    public event Action OnGoodPotion;
    public event Action OnBadPotion;
    public event Action OnNormalPotion;
    public event Action OnResetClaudron;

    [SerializeField] private TimerScript _timer;
    [SerializeField] private MonoLimitedNumber _counter;
    [SerializeField] private ButtonEventCatcher _button;
    [SerializeField] private WidgetClicker _widgetClicker;

    [Space] 
    [SerializeField, ] private ButtonMode _buttonMode;
  
    [SerializeField] private float _maxCooldownTimeAllowed = 1.5f;
    [SerializeField] private int _decrementStep = 1;
    [SerializeField] private float _decrementTimeDelay = 0.5f;

    [Space]
    [SerializeField] private int _timerDuration = 10;
    [SerializeField] private int _maxClickCounterValue = 20;
    [SerializeField] private int _incrementStep = 1;
    [SerializeField] private float _indicatorChangeTime = 0.5f;
    //[SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float _resetDelayTime = 5f;
    [SerializeField] private float _incrementPauseTime = 1f;


    [Space] 
    [SerializeField] private ProgressbarPart[] _progressbarParts;

    private bool _isCooking;
    private bool _isPauseTimeBefore;
    private float _lastTimeButtonPressed;

    public float ResetDelayTime => _resetDelayTime;
    private void OnEnable()
    {
        if (_buttonMode == PRESS_BUTTON)
        {
            _button.OnButtonPressed += HandleButtonPress;          
        }

        if (_buttonMode == HOLD_BUTTON)
        {
            _button.OnButtonHold += HandleButtonHold;
            _button.OnButtonRelease += HandleButtonRelease;
        }

        _timer.Duration = _timerDuration;
        _timer.OnFinished += FailCoocking;
        
        _counter.Setup(0, _maxClickCounterValue);
        _counter.OnValueChanged += IndicateCountChange;
    }

    private void OnDisable()
    {
        if (_buttonMode == PRESS_BUTTON)
        {
            _button.OnButtonPressed -= HandleButtonPress;
        }

        if (_buttonMode == HOLD_BUTTON)
        {
            _button.OnButtonRelease -= HandleButtonRelease;
            _button.OnButtonHold -= HandleButtonHold;
        }

        _timer.OnFinished -= FailCoocking;
        _counter.OnValueChanged -= IndicateCountChange;
        
        StopAllCoroutines();
    }

    private void HandleButtonHold()
    {
        PrepareCooking();
        InvokeRepeating(nameof(IncrementWhileHold), 0, _incrementPauseTime);
    }

    private void IncrementWhileHold()
    {
        Increment(_incrementStep);
    }

    private void HandleButtonRelease()
    {

        CheckResult();
    }

    private void Awake()
    {
        InitializeProgressBar();
    }

    private IEnumerator DecrementTillZero()
    {
        while (_isCooking)
        {
            Decrement(_decrementStep);
            ShowMessage((_timer.Duration - _timer.CurrentTime).ToString("0"));
            yield return new WaitForSeconds(_decrementTimeDelay);
        }
    }

    private IEnumerator CheckStopButtonPress()
    {
        while (_isCooking)
        {
            if (Time.time >= _lastTimeButtonPressed + _maxCooldownTimeAllowed)
            {
                CheckResult();
            }
            yield return null;
        }
    }

    private void HandleButtonPress()
    {
        if (_isPauseTimeBefore) return;
        if (!_isCooking) StartCooking();
        
        Increment(_incrementStep);
        _lastTimeButtonPressed = Time.time;
    }

    private void StartCooking()
    {
        PrepareCooking();

        if (_buttonMode == PRESS_BUTTON)
        {
            StartCoroutine(nameof(DecrementTillZero));
            StartCoroutine(nameof(CheckStopButtonPress));
        }
    }

    private void PrepareCooking()
    {
        _widgetClicker.ClearProgress();
        _timer.Play();
        _lastTimeButtonPressed = Time.time;
        _isCooking = true;
    }

    private void CheckResult()
    {
        Stop();

        /*DOVirtual.DelayedCall(_indicatorChangeTime, () => 
        {
            var progressPerSegment = _maxClickCounterValue / _progressbarParts.Length;
            var currentPartNumber = (_counter.Value / progressPerSegment) - 1;
            var currentPart = _progressbarParts[currentPartNumber];
        
            ShowMessage(currentPart.InfoText); 
            
            DOVirtual.DelayedCall(_resetDelayTime, () => Reset());
        });*/
        
        var progressPerSegment = _maxClickCounterValue / _progressbarParts.Length;
        var currentPartNumber = (_counter.Value / progressPerSegment);
        var currentPart = _progressbarParts[currentPartNumber];

        if(currentPart.PotionState == PotionState.NORMAL ||
           currentPart.PotionState ==  PotionState.GOOD)
        {
            OnGoodPotion?.Invoke();
        }
        else
        {
            OnBadPotion?.Invoke();
        }
        

        DOVirtual.DelayedCall(_resetDelayTime, () => Reset());
    }

    private void ShowMessage(string message)
    {
        _widgetClicker.Text.text = message;
    }

    /*[Button("Stop coocking"), GUIColor(0,1,0)]*/
    private void FailCoocking()
    {      
        Stop();
    }

    public void Reset()
    {
        DOVirtual.DelayedCall(0.5f, () =>
        {
            _counter.Setup(0, _maxClickCounterValue);
            _widgetClicker.ClearProgress();
            ShowMessage(String.Empty);
            _isPauseTimeBefore = false;
            _button.Button.interactable = true;
            OnResetClaudron?.Invoke();
        });
    }

    private void Stop()
    {
        StopAllCoroutines();
        CancelInvoke();
        _isCooking = false;
        _isPauseTimeBefore = true;
        _timer.Cancel();
        _button.Button.interactable = false;       
        
        DOVirtual.DelayedCall(_resetDelayTime, () => Reset());
    }

    private void Increment(int value)
    {
        _counter.Increment(value);
    }

    private void Decrement(int value)
    {
        _counter.Decrement(value);
    }

    private void IndicateCountChange(int previousvalue, int newvalue)
    {
        DOVirtual.Float(previousvalue, newvalue, _indicatorChangeTime, value =>
        {
            var currentProgress = (float)value / _maxClickCounterValue;
            UpdateProgressBar(currentProgress);
        })/*.SetEase(_animationCurve)*/;
    }

    private void UpdateProgressBar(float progress)
    {
       _widgetClicker.IndicateProgress(progress);
    }

    private void InitializeProgressBar()
    {
        var numberOfSegments = _progressbarParts.Length;
        var colors = _progressbarParts.Select(p => p.Color).ToArray();
        _widgetClicker.InitializeProgressBar(numberOfSegments, colors);
    }

    public void ChangeResetDelayTime(float numb)
    {
        _resetDelayTime = numb;
    }
}

public enum ButtonMode
{
    HOLD_BUTTON =0,
    PRESS_BUTTON = 1
}
