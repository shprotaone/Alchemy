using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;

public class Visitor : MonoBehaviour
{  
    [SerializeField] private GuildsType _currentGuild;
    [SerializeField] private VisitorController _visitorController;
    [SerializeField] private PotionTaskView _currentTask;
    [SerializeField] private TMP_Text _timerText;

    private SpriteRenderer _visitorImage;
    private PotionTask _task;

    private bool _firstTask = true;
    private float _timeVisitor;


    public GuildsType Guild => _currentGuild;
    public PotionTaskView TaskView => _currentTask;

    private void Awake()
    {
        _visitorController.OnVisitorOut += Fading;
        _timeVisitor = _visitorController.VisitorTime;
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
            if (bottle.IsFull && _task.ChekResult(bottle.PotionInBottle))
            {
                if (_firstTask)
                {
                    _firstTask = false;
                }

                bottle.ReturnEffect();
                bottle.DestroyBottle();
            }
        }
    }

    private IEnumerator Timer()
    {
        if (!_visitorController.FirstVisitor)
        {
            float counter = _timeVisitor;
            _timerText.gameObject.SetActive(true);
            UpdateTimerDisplay(counter);

            while (counter > 0)
            {
                yield return new WaitForSeconds(1);
                counter--;
                UpdateTimerDisplay(counter);
            }

            if (counter == 0)
            {
                Fading();
                _currentTask.TaskCanceled();
            }
            yield break;
        }
        else
        {
            _timerText.gameObject.SetActive(false);
        }              
    }

    private void UpdateTimerDisplay(float value)
    {
        float seconds = Mathf.FloorToInt(value % 60);
        _timerText.text = seconds.ToString();
    }

    public void Rising(PotionTask task)
    {
        _task = new PotionTask(task);
        _currentTask.InitTask(_task);

        SetTime();

        StartCoroutine(Timer());
        this.gameObject.SetActive(true);                 
        
        DOTween.ToAlpha(()=> _visitorImage.color, x => _visitorImage.color = x, 1, 1);
    }

    private void SetTime()
    {
        if (_task.CurrentPotion.Contraband)
        {
            _timeVisitor = _visitorController.VisitorContrabandTime;
        }
        else
        {
            _timeVisitor = _visitorController.VisitorTime;
        }
    }

    public void Fading()
    {        
        _currentTask.FadingTask();
        StopAllCoroutines();

        _timerText.gameObject.SetActive(false);

        DOTween.ToAlpha(() => _visitorImage.color, x => _visitorImage.color = x, 0, 1).OnComplete(() => this.gameObject.SetActive(false));
              
    }

    public void BrightVisitor(string sortingLayerName)
    {
        _visitorImage.sortingLayerName = sortingLayerName;
        this.GetComponentInChildren<Canvas>().sortingLayerName = sortingLayerName;
    }

    private void OnDisable()
    {
        _visitorController.OnVisitorOut -= Fading;
        _task = null;
    }
}
