using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerSubject : MonoBehaviour
{
    public Subject subject;

    [SerializeField] private FirstPlayObserver _firstPlayObserver;

    private void Start()
    {
        subject = new Subject();
        subject.AddObserver(_firstPlayObserver);
    }

    public void Notify()
    {
        subject.Notify();
    }
}
