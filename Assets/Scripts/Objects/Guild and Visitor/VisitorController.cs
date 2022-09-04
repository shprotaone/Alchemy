using System;
using UnityEngine;

public class VisitorController : MonoBehaviour
{    
    public Action OnVisitorCall;
    public Action OnVisitorOut;

    [SerializeField] private Visitor[] _visitors;
    [SerializeField] private AudioClip _visitorFadingSound;
    
    private Visitor _currentVisitor;
    private Visitor _prevVisitor;
    private AudioSource _audioSource;

    private int _visitorCounter;
    private bool _firstVisitor = true;
    private bool _shopIsOpen;

    public Visitor CurrentVisitor => _currentVisitor;
    public bool FirstVisitor => _firstVisitor;

    public bool ShopIsOpen => _shopIsOpen;

    public void InitVisitorController()
    {
        _audioSource = GetComponent<AudioSource>();
        _shopIsOpen = true;
      
        OnVisitorCall += CallVisitor;
        OnVisitorOut += VisitorGoOutSound;
        
        ShopControl(_shopIsOpen);

    }

    public void ShopControl(bool flag)
    {
        _shopIsOpen = flag;

        if (_shopIsOpen)
            OnVisitorCall?.Invoke();
        else
            DisableAllVisitors();
    }

    public void CallVisitor()
    {
        int visitorCount = UnityEngine.Random.Range(0, _visitors.Length);

        if (_shopIsOpen)
        {
            _currentVisitor = _visitors[visitorCount];

            if (_currentVisitor == _prevVisitor)
            {
                visitorCount++;

                if(visitorCount == _visitors.Length)
                {
                    _currentVisitor = _visitors[0];
                }
                else
                {
                    _currentVisitor = _visitors[visitorCount];
                }
                
            }

            _currentVisitor.gameObject.SetActive(true);
            _currentVisitor.Rising();
            _audioSource.Play();
           
            _prevVisitor = _currentVisitor;           
        }        
    }

    public void VisitorGoOutSound()
    {
        _audioSource.PlayOneShot(_visitorFadingSound);
    }

    public void DisableVisitor()
    {
        if (_currentVisitor != null)
        {
            OnVisitorOut?.Invoke();
            _firstVisitor = false;

            _currentVisitor.Fading();
            OnVisitorCall?.Invoke();
        }
    }  

    private void DisableAllVisitors()
    {
        foreach (Visitor visitor in _visitors)
        {
            if (visitor.gameObject.activeInHierarchy)
            {
                visitor.Fading();
            }
        }
    }

    private void OnDisable()
    {        
        OnVisitorCall -= CallVisitor;
        OnVisitorOut -= VisitorGoOutSound;
    }
}
