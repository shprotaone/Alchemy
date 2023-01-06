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

    public Visitor CurrentVisitor { get; private set; }
    public bool IsActive { get; private set; }

    public void InitVisitorController(PotionTaskSystem taskSystem,int visitorCount)
    {
        _audioSource = GetComponent<AudioSource>();
        _taskSystem = taskSystem;
        IsActive = true;
        _visitorCountSystem = new VisitorCountSystem(_countView,visitorCount);
        _visitorCountSystem.OnVisitorEnded += _gameManager.CompleteLevel;

    }

    public void Activate()
    {
        VisitorChoice();
        SetNextTask(_taskSystem.GetTaskv2());
        CallVisitor(_currentTask);
    }

    public void SetNextTask(PotionTask task)
    {
        _currentTask = task;
    }

    public void CallVisitor(PotionTask task)
    {       
        if (IsActive)
        {
            CurrentVisitor.gameObject.SetActive(true);
            CurrentVisitor.Init(task);
            _audioSource.Play();
           
            _prevVisitor = CurrentVisitor;           
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

            VisitorChoice();
            SetNextTask(_taskSystem.GetTaskv2());           
            CallVisitor(_currentTask);
            _visitorCountSystem.DecreaseVisitorCount();
        }
    }  
}
