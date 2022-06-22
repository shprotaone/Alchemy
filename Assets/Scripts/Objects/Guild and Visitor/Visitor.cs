using System.Collections;
using UnityEngine;
using TMPro;

public class Visitor : MonoBehaviour
{
    private const float stockTime = 50;

    [SerializeField] private GuildsType _currentGuild;
    [SerializeField] private Transform _visitorControllerTransform;
    [SerializeField] private Task _currentTask;
    [SerializeField] private TMP_Text _timerText;
    
    private SpriteRenderer[] _visitorImages;
    private TMP_Text[] _taskText;
    private VisitorController _visitorController;
    private float _timer;

    public GuildsType Guild => _currentGuild;

    private void OnEnable()
    {
        _visitorController = _visitorControllerTransform.GetComponentInParent<VisitorController>();
        _visitorImages = GetComponentsInChildren<SpriteRenderer>();
        _taskText = GetComponentsInChildren<TMP_Text>();

        SetAlpha(0);
        
        _timer = stockTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bottle"))
        {
            Bottle bottle = collision.GetComponent<Bottle>();
            if (bottle.IsFull && _currentTask.ChekResult(bottle.PotionInBottle))
            {
                bottle.ResetBottle();
            }
        }
    }

    private void Update()
    {
        Timer();
    }

    public void Rising()
    {       
        StartCoroutine(Rise());
        _currentTask.InitTask();
    }

    public void Fading()
    {
        _visitorController.CallVisitor();
        _visitorController.VisitorGoOutSound();
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

    private void ChangeAlphaColor(float value,SpriteRenderer sprite)
    {
        Color color = sprite.color;
        color.a = value;
        sprite.color = color;
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
            UpdateTimerDisplay(_timer);
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

    private void UpdateTimerDisplay(float value)
    {
        float seconds = Mathf.FloorToInt(value % 60);
        _timerText.text = seconds.ToString();
    }
}
