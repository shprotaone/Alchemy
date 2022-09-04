using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Step : MonoBehaviour
{
    [SerializeField] protected TutorialManager _tutorialManager;
    [SerializeField] protected DialogView _dialogView;

    [TextArea]
    [SerializeField] protected string _text;

    /// <summary>
    /// ����������� � ������ ��������� Step
    /// </summary>
    public virtual void StepAction()
    {

    }
    /// <summary>
    /// ������������ ����� ������ Step
    /// </summary>
    public virtual void EndStepAction()
    {

    }

    public void EnableStep()
    {
        _dialogView.gameObject.SetActive(true);
        _dialogView.DialogText.text = _text;
        StepAction();
    }

    public void DisableStep()
    {
        _dialogView.gameObject.SetActive(false);
        gameObject.SetActive(false);
        EndStepAction();
    }
}
