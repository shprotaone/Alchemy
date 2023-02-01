using System.Collections.Generic;
using UnityEngine;

public class Subject
{
    private List<IObserver> _observer;

    public Subject()
    {
        _observer = new List<IObserver>();
    }

    public void AddObserver(IObserver obs)
    {
        _observer.Add(obs);
    }

    public void RemoveObserver(IObserver obs)
    {
        _observer.Remove(obs);
    }

    public void Notify()
    {
        for (int i = 0; i < _observer.Count; i++)
        {
            _observer[i].Notify(this,"Event");
        }
    }

    public void Notify(string text)
    {
        for (int i = 0; i < _observer.Count; i++)
        {
            _observer[i].Notify(this, text);
        }
    }
}
