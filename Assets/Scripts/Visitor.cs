using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Visitor : MonoBehaviour
{
    [SerializeField] private GuildsType _currentGuild;
    [SerializeField] private Transform _visitorControllerTransform;

    private const float stockTime = 50;
    private Image[] _visitorImages;
    private TMP_Text[] _taskText;

    private VisitorController _visitorController;
    private Task _currentTask;
    private float _timer;

    public GuildsType Guild => _currentGuild;

    private void Awake()
    {
        _visitorController = _visitorControllerTransform.GetComponentInParent<VisitorController>();
        _currentTask = GetComponentInChildren<Task>();
        _visitorImages = GetComponentsInChildren<Image>();
        _taskText = GetComponentsInChildren<TMP_Text>();

        SetAlpha(0);
        
        _timer = stockTime;
    }

    private void Update()
    {
        Timer();
    }

    public void Rising()
    {
        _currentTask.InitTask();
        StartCoroutine(Rise());
    }

    public void Fading()
    {
        _visitorController.CallVisitor();
        StartCoroutine(Fade());       
    }

    private IEnumerator Rise()
    {
        this.gameObject.SetActive(true);

        for (float i = 0.05f; i <= 1; i+= 0.05f)
        {
            foreach (var item in _visitorImages)
            {
                ChangeAlphaColor(i, item);                
            }

            foreach (var item in _taskText)
            {
                ChangeAlphaColor(i, item);
            }
            
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator Fade()
    {
        for (float i = 1; i >= 0.05f; i -= 0.05f)
        {
            foreach (var item in _visitorImages)
            {
                ChangeAlphaColor(i, item);
            }

            foreach (var item in _taskText)
            {
                ChangeAlphaColor(i, item);
            }

            yield return new WaitForSeconds(0.05f);
        }
        ResetTimer();

        this.gameObject.SetActive(false);

    }

    private void ChangeAlphaColor(float value,Image image)
    {
        Color color = image.color;
        color.a = value;
        image.color = color;
    }

    private void ChangeAlphaColor(float value, TMP_Text text)
    {
        Color color = text.color;
        color.a = value;
        text.color = color;
    }

    private void Timer()
    {
        if (_timer > 0.02)
        {
            _timer -= Time.deltaTime;
            _visitorController.UpdateTimerDisplay(_timer);
        }
        else
        {
            Fading();
            _currentTask.TaskCanceled();
            ResetTimer();
        }
    }

    private void ResetTimer()
    {
        _timer = stockTime;
    }

    private void SetAlpha(int value)
    {
        foreach (var item in _visitorImages)
        {
            ChangeAlphaColor(value, item);
        }

        foreach (var item in _taskText)
        {
            ChangeAlphaColor(value, item);
        }
    }

}
