using DG.Tweening;
using UnityEngine;

public class VisitorController : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private VisitorCountSystemView _countView;
    [SerializeField] private Visitor[] _visitors;

    private VisitorCountSystem _visitorCountSystem;
    private AudioManager _audioManager;
    private PotionTaskSystem _taskSystem;
    private PotionTask _currentTask;
    private Visitor _prevVisitor;

    public Visitor CurrentVisitor { get; private set; }
    public bool IsActive { get; private set; }

    public void InitVisitorController(PotionTaskSystem taskSystem,int visitorCount,AudioManager audioManager)
    {
        _audioManager = audioManager;
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

    private void SetNextTask(PotionTask task)
    {
        _currentTask = task;
    }

    public void GoAwayVisitor()
    {
        CurrentVisitor.TaskView.DisableLables(true);
        CurrentVisitor.ShowEmoji();
        DOVirtual.DelayedCall(0.5f, DisableVisitor);
    }
    public void FillEmojiStatus(float index)
    {
        CurrentVisitor.SetEmoji(index);
    }

    public void CallVisitor()
    {       
        if (_visitorCountSystem.VisitorLeft > 0)
        {           
            VisitorChoice();
            SetNextTask(_taskSystem.GetTaskv2());

            CurrentVisitor.gameObject.SetActive(true);
            CurrentVisitor.Init(_currentTask);
           
            _audioManager.PlaySFX(_audioManager.GetRandomSound(_audioManager.Data.DoorOpenClips));

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
        _audioManager.PlaySFX(_audioManager.Data.Closed);
    }

    private void DisableVisitor()
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
