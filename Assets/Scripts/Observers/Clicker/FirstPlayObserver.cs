using UnityEngine;

public class FirstPlayObserver : MonoBehaviour, IObserver {
    [SerializeField] private GuideController _guideController;
    [SerializeField] private ImageBlink _blink;
    public void Notify(object obj, string text)
    {
        //Deactivate();    
    }

    public void Activate()
    {
        _blink.StartBlink();
        _guideController.Activate();
    }

    public void Deactivate()
    {
        _blink.DisableBlink();
    }
}
