using System;
using UnityEngine;

public class VisitorController : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private VisitorCountSystemView _countView;
    [SerializeField] private Visitor[] _visitors;
    [SerializeField] private AudioClip _visitorFadingSound;

    private VisitorCountSystem _visitorCountSystem;
    private PotionTaskSystem _taskSystem;
    private PotionTask _currentTask;
    private Visitor _prevVisitor;
    private AudioSource _audioSource;

    private int _visitorCounter;
    private bool _firstVisitor = true;

    public PotionTask CurrentTask => _currentTask;
    public Visitor CurrentVisitor { get; private set; }
    public int VisitorTime { get; private set; }
    public int VisitorContrabandTime { get; private set; }
    public bool FirstVisitor { get; private set; }
    public bool IsActive { get; private set; }

    public void InitVisitorController(PotionTaskSystem taskSystem, int visitorTime, int visitorContrabandTime,int visitorCount)
    {
        _audioSource = GetComponent<AudioSource>();
        _taskSystem = taskSystem;
        IsActive = true;
        _visitorCountSystem = new VisitorCountSystem(_countView,visitorCount);
        _visitorCountSystem.OnVisitorEnded += _gameManager.CompleteLevel;

        VisitorTime = visitorTime;
    }

    public void Activate()
    {
        VisitorChoice();
        SetNextTask(_taskSystem.GetTaskv2());
        CallVisitor(_currentTask);
        SetVisitorTime(VisitorTime, VisitorContrabandTime);
        ShopControl(IsActive);
    }

    public void Deactivate()
    {
        ShopControl(false);
    }

    public void SetNextTask(PotionTask task)
    {
        _currentTask = task;
    }

    public void ShopControl(bool flag)
    {
        //IsActive = flag;

        //if (IsActive)
        //    CallVisitor(_currentTask);
        //else
        //    DisableAllVisitors();
    }

    public void CallVisitor(PotionTask task)
    {       
        if (IsActive)
        {
            CurrentVisitor.gameObject.SetActive(true);
            CurrentVisitor.Init(this,task);
            _audioSource.Play();
           
            _prevVisitor = CurrentVisitor;           
        }        
    }

    private void SetVisitorTime(int visitorTime, int visitorContrabandTime)
    {
        if (visitorTime != 0)
        {
            VisitorTime = visitorTime;
            VisitorContrabandTime = visitorContrabandTime;
        }
        else
        {
            Debug.LogWarning("Не установлено время, ставлю стандартное значение - 50");
            VisitorTime = 50;
            VisitorContrabandTime = 10;
        }
    }

    private void VisitorChoice()
    {
        int visitorCount = UnityEngine.Random.Range(0, _visitors.Length);

        CurrentVisitor = _visitors[visitorCount];

        if (CurrentVisitor == _prevVisitor)
        {
            visitorCount++;

            if (visitorCount == _visitors.Length)
            {
                CurrentVisitor = _visitors[0];
            }
            else
            {
                CurrentVisitor = _visitors[visitorCount];
            }
        }
    }

    public void VisitorGoOutSound()
    {
        _audioSource.PlayOneShot(_visitorFadingSound);
    }

    public void DisableVisitor()
    {
        if (CurrentVisitor != null)
        {
            CurrentVisitor.VisitorView.Fading();
            CurrentVisitor.TaskView.FadingTask();

            VisitorGoOutSound();
            _firstVisitor = false;

            VisitorChoice();
            SetNextTask(_taskSystem.GetTaskv2());           
            CallVisitor(_currentTask);
            _visitorCountSystem.DecreaseVisitorCount();
        }
    }  

    private void DisableAllVisitors()
    {
        foreach (Visitor visitor in _visitors)
        {
            if (visitor.gameObject.activeInHierarchy)
            {
                visitor.VisitorView.Fading();
            }
        }
    }
}
