using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public sealed class WidgetClicker : MonoBehaviour
{
    [SerializeField] private MyCircularProgressBar360 _circularProgressBar;
    [SerializeField] private TextMeshProUGUI _text;

    public MyCircularProgressBar360 CircularProgressBar => _circularProgressBar;
    public TextMeshProUGUI Text => _text;

    public void InitializeProgressBar(int length, Color[] colors)
    {
        var widget = CircularProgressBar;
        widget.SetUpProgressBar(length, colors);
    }

    public void IndicateProgress(float progress)
    {
        _circularProgressBar.FillAmount = progress;
    }

    public void ClearProgress()
    {
        _circularProgressBar.FillAmount = 0f;
    }
}
