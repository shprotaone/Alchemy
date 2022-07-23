using System;
using UnityEngine;
using DG.Tweening;

public class VisitorController : MonoBehaviour
{    
    public static Action OnVisitorCall;
    public static Action OnVisitorOut;

    [SerializeField] private Visitor[] _visitors;
    [SerializeField] private AudioClip _visitorFadingSound;    
    [SerializeField] private bool _shopIsOpen = false;

    private Visitor _currentVisitor;
    private Visitor _prevVisitor;
    private AudioSource _audioSource;
    private bool _isCall;
    
    public Visitor CurrentVisitor => _currentVisitor;

    public bool ShopIsOpen 
    { 
        get { return _shopIsOpen; }
        set 
        { 
            _shopIsOpen = value;

            if (_shopIsOpen)
                CallVisitor();
            else
                OnVisitorOut?.Invoke();
        }    
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        OnVisitorCall += CallVisitor;
        OnVisitorOut += VisitorGoOutSound;

        if (_shopIsOpen)
            OnVisitorCall?.Invoke();
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

            _currentVisitor.Fading();
            OnVisitorCall?.Invoke();
        }
    }  

    private void OnDisable()
    {        
        OnVisitorCall -= CallVisitor;
        OnVisitorOut -= VisitorGoOutSound;
    }
}
