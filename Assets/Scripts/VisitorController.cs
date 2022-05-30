using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VisitorController : MonoBehaviour
{
    private const float taskTime = 60;

    [SerializeField] private Visitor[] _visitors;
    [SerializeField] private TMP_Text _timerText;

    private Visitor _currentVisitor;
    
    private float _currentTimer;

    private void Start()
    {
        StartCoroutine(Timer());
    }

    private void CallVisitor()
    {
        _currentVisitor = _visitors[Random.Range(0, _visitors.Length)];

        _currentVisitor.gameObject.SetActive(true);
        _currentVisitor.Rising();
    }

    private void DisableVisitor()
    {
        _currentVisitor.Fading();
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        CallVisitor();
        _currentTimer = taskTime;
        while (_currentTimer > 0)
        {
            _currentTimer--;
            _timerText.text = "Time Left " + _currentTimer.ToString();
            yield return new WaitForSeconds(1f);
        }
        DisableVisitor();
    }
}
