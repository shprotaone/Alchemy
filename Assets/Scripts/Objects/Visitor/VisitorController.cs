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
    }

    public void Activate()
    {
        VisitorChoice();
        SetNextTask(_taskSystem.GetTaskv2());
        CallVisitor();
    }

    public void SetNextTask(PotionTask task)
    {
        _currentTask = task;
    }

    public void CallVisitor()
    {
        if (_visitorCountSystem.VisitorLeft > 0)
        {
            VisitorChoice();
            SetNextTask(_taskSystem.GetTaskv2());

            CurrentVisitor.gameObject.SetActive(true);
            CurrentVisitor.Init(_currentTask);
            _audioSource.Play();

            _prevVisitor = CurrentVisitor;
        }
        else
        {
            _gameManager.CompleteLevel();
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
            CurrentVisitor.Disable();
            VisitorGoOutSound();
            _visitorCountSystem.DecreaseVisitorCount();
        }
    }

    public void Disable()
    {
        foreach (Visitor visitor in _visitors)
        {
            visitor.Disable();
        }
    }
}
