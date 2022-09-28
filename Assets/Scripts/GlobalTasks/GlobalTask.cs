using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class GlobalTask : MonoBehaviour
{
    public static Action OnLevelComplete;

    [TextArea(5,10)]
    [SerializeField] protected string _goalText;

    [SerializeField] protected TMP_Text _taskText;
    [SerializeField] protected GameManager _gameManager;

    public abstract void SetTaskValue(int value, int minValue);
    public abstract void Init();
    public abstract void SpecialSelection();
    public abstract void CheckMoneyTask();

    public void DisableTask()
    {
        _taskText.text = "";
    }

    protected void SetLevelTaskText()
    {
        _taskText.text = _goalText;
    }
}
