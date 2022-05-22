using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Visitor : MonoBehaviour
{
    private Image[] _visitorImages;
    private TMP_Text[] _taskText;

    private Task _currentTask;

    private void Start()
    {
        _currentTask = GetComponentInChildren<Task>();
        _visitorImages = GetComponentsInChildren<Image>();
        foreach (var item in _visitorImages)
        {
            ChangeAlphaColor(0, item);
        }

        _taskText = GetComponentsInChildren<TMP_Text>();

        foreach (var item in _taskText)
        {
            ChangeAlphaColor(0, item);
        }
    }

    public void StartFading()
    {
        _currentTask.InitTask();
        StartCoroutine(Rise());
    }

    private IEnumerator Rise()
    {
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

    private void ChangeAlphaColor(float value,Image image)
    {
        Color color = image.color;
        color.a = value;
        image.color = color;
    }

    private void ChangeAlphaColor(float value, TMP_Text text)
    {
        Color color = text.color;
        color.a = value;
        text.color = color;
    }
}
