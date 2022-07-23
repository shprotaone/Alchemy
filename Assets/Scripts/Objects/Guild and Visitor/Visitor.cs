using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;

public class Visitor : MonoBehaviour
{
    private const float stockTime = 50;

    [SerializeField] private GuildsType _currentGuild;
    [SerializeField] private VisitorController _visitorController;
    [SerializeField] private PotionTask _currentTask;
    [SerializeField] private TMP_Text _timerText;

    private SpriteRenderer _visitorImage;

    private bool _firstTask = true;

    public GuildsType Guild => _currentGuild;

    private void Awake()
    {
        VisitorController.OnVisitorOut += Fading;
    }

    private void OnEnable()
    {
        _visitorImage = GetComponentInChildren<SpriteRenderer>();       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bottle"))
        {
            Bottle bottle = collision.GetComponent<Bottle>();
            if (bottle.IsFull && _currentTask.ChekResult(bottle.PotionInBottle))
            {
                if (_firstTask)
                {
                    bottle.GetComponent<NextCountHandler>().DisableClickHerePrefab();   //сомнительно
                    _firstTask = false;
                }
                
                bottle.ResetBottle();                
                bottle.Movement();
            }
        }
    }

    private IEnumerator Timer()
    {
        float counter = stockTime;
        UpdateTimerDisplay(counter);

        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
            UpdateTimerDisplay(counter);
        }
        
        if(counter == 0)
        {
            Fading();
            _currentTask.TaskCanceled();
        }

        yield break;
    }

    private void UpdateTimerDisplay(float value)
    {
        float seconds = Mathf.FloorToInt(value % 60);
        _timerText.text = seconds.ToString();
    }

    public void Rising()
    {
        StartCoroutine(Timer());
        this.gameObject.SetActive(true);
        _timerText.gameObject.SetActive(true);
       
        _currentTask.InitTask();
        
        DOTween.ToAlpha(()=> _visitorImage.color, x => _visitorImage.color = x, 1, 1);
    }

    public void Fading()
    {        
        //this.gameObject.SetActive(true);

        _currentTask.FadingTask();
        StopAllCoroutines();

        _timerText.gameObject.SetActive(false);

        DOTween.ToAlpha(() => _visitorImage.color, x => _visitorImage.color = x, 0, 1).OnComplete(() => this.gameObject.SetActive(false));
              
    }

    private void OnDisable()
    {
        VisitorController.OnVisitorOut -= Fading;
    }
}
