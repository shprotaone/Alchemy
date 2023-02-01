using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNotifyObserver : MonoBehaviour,IObserver
{
    [SerializeField] private DayNotifyView _view;

    public void Notify(object obj, string text)
    {
        _view.Init();
        _view.Show(text);
    }

}
