using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using LimitedNumbers;
using UnityEngine;
using static ButtonMode;

public sealed class ClickController : MonoBehaviour
{
    public event Action OnGoodPotion;
    public event Action OnBadPotion;

    [SerializeField] private ClickerSubject _clickerSubject;
    [SerializeField] private TimerScript _timer;
    [SerializeField] private MonoLimitedNumber _counter;
    [SerializeField] private ButtonEventCatcher _button;
    [SerializeField] private WidgetClicker _widgetClicker;
    [SerializeField] private ClaudronSystem _claudronSystem;
    [SerializeField] private CookButton _cookButton;
    [SerializeField] private ClaudronEffectController _claudronEffectController;

    [Space]
    [SerializeField] private ButtonMode _buttonMode;

    [SerializeField] private float _maxCooldownTimeAllowed = 1.5f;
    [SerializeField] private int _decrementStep = 1;
    [SerializeField] private float _decrementTimeDelay = 0.5f;

    [Space]
    [SerializeField] private int _timerDuration = 10;
    [SerializeField] private int _maxClickCounterValue = 20;
    [SerializeField] private int _incrementStep = 1;
    [SerializeField] private float _indicatorChangeTime = 0.5f;
    //[SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float _resetFailDelayTime = 5f;
    [SerializeField] private float _resetCompleteDelayTime;
    [SerializeField] private float _incrementPauseTime = 1f;

    [Space]
    [SerializeField] private int _countParts;
    [SerializeField] private List<ProgressbarPart> _currentParts;
    [SerializeField] private ProgressbarPart _badPart;
    [SerializeField] private ProgressbarPart _goodPart;

    private Tween _incrementTween;
    private bool _isCooking;
    private bool _isPauseTimeBefore;
    private float _lastTimeButtonPressed;
    private float _delayTime;

    private bool _isStartHold;
    public float ResetDelayTime => _resetFailDelayTime;
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

        OnBadPotion += FailCoocking;
        OnGoodPotion += CompleteCoocking;
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

        OnBadPotion -= FailCoocking;
        OnGoodPotion -= CompleteCoocking;

        StopAllCoroutines();
    }

    private void HandleButtonHold()
    {       
        _claudronEffectController.Boil();
        PrepareCooking();
        _clickerSubject.Notify();
        //InvokeRepeating(nameof(IncrementWhileHold), 0, _incrementPauseTime);
        if (!_isStartHold)
        {
            _incrementTween = DOVirtual.DelayedCall(0, IncrementWhileHold, false).SetLoops(-1).SetUpdate(UpdateType.Fixed)
                                                                                 .OnKill(() => _isStartHold = false);
            _isStartHold = true;
        }
        
    }

    private void IncrementWhileHold()
    {
        Increment(_incrementStep);
    }

    private void HandleButtonRelease()
    {
        CheckResult();
        
        _incrementTween.Kill();
        _incrementTween.Rewind();
        _claudronEffectController.StopBoil();
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

        float progressPerSegment = (float)_maxClickCounterValue / _currentParts.Count;
        float  resultSegment = _counter.Value / progressPerSegment;

        var currentPartNumber = Mathf.FloorToInt(resultSegment);
        if (currentPartNumber < _currentParts.Count)
        {
            var currentPart = _currentParts[currentPartNumber];
            if (currentPart.PotionState == PotionState.NORMAL ||
               currentPart.PotionState == PotionState.GOOD)
            {
                OnGoodPotion?.Invoke();
            }
            else
            {
                OnBadPotion?.Invoke();
            }
        }
        else
        {
            FailCoocking();
        }

    }

    private void ShowMessage(string message)
    {
        _widgetClicker.Text.text = message;
    }

    /*[Button("Stop coocking"), GUIColor(0,1,0)]*/
    private void FailCoocking()
    {
        _delayTime = _resetFailDelayTime;
        _cookButton.ResetButtonImage(_delayTime);
        Stop();

        DOVirtual.DelayedCall(_delayTime, () => Reset());
    }

    private void CompleteCoocking()
    {
        _delayTime = _resetCompleteDelayTime;
        Stop();

        DOVirtual.DelayedCall(_delayTime, () => Reset());
    }

    public void Reset()
    {
        _counter.Setup(0, _maxClickCounterValue);
        _widgetClicker.ClearProgress();
        ShowMessage(String.Empty);
        _isPauseTimeBefore = false;

        _claudronSystem.ClaudronButtonState();
    }

    private void Stop()
    {
        StopAllCoroutines();
        CancelInvoke();
        _isCooking = false;
        _isPauseTimeBefore = true;
        _timer.Cancel();       
        _button.Button.interactable = false;
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

    public void InitializeProgressBar()
    {
        ProgressBarRandomize();

        var numberOfSegments = _currentParts.Count;
        var colors = _currentParts.Select(p => p.Color).ToArray();
        _widgetClicker.InitializeProgressBar(numberOfSegments, colors);
    }

    private void ProgressBarRandomize()
    {
        _currentParts = new List<ProgressbarPart>();
        for (int i = 1; i < _countParts; i++)
        {
            _currentParts.Add(_badPart);
        }

        int goodIndexPart = UnityEngine.Random.Range(_currentParts.Count / 2, _currentParts.Count);
        _currentParts.Insert(goodIndexPart, _goodPart);
    }

    public void Disable()
    {
        _currentParts.Clear();
        InitializeProgressBar();
    }
}

public enum ButtonMode
{
    HOLD_BUTTON = 0,
    PRESS_BUTTON = 1
}
