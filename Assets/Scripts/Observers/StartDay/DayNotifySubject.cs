using System.Collections.Generic;
using UnityEngine;

public class DayNotifySubject : MonoBehaviour
{
    public Subject subject;
    [SerializeField] private DayNotifyObserver _dayNotifyObserver;
    [SerializeField] private DayNotifyTextVariant _textVariant;

    private string _textResult;
    private bool _isActive;

    private void Start()
    {
        subject = new Subject();
        subject.AddObserver(_dayNotifyObserver);
    }

    public void CallNotify()
    {
        if(_isActive)
        {
            _dayNotifyObserver.Notify(this, _textResult);
        }      
    }

    public void SetNotify(PotionLabelType potionLabelType, bool isActive)
    {
        SetRandomText(potionLabelType);      
        _isActive = isActive;
    }

    private void SetRandomText(PotionLabelType label)
    {
        if(label == PotionLabelType.WATER)
        {
            _textResult = GetRandomNotify(_textVariant.waterTexts);
        }
        else if(label == PotionLabelType.FIRE)
        {
            _textResult = GetRandomNotify(_textVariant.fireTexts);
        }
        else if(label == PotionLabelType.ROCK)
        {
            _textResult = GetRandomNotify(_textVariant.stoneTexsts);
        }
    }

    private string GetRandomNotify(List<string> textList)
    {
        int randomIndex = Random.Range(0, textList.Count - 1);
        return textList[randomIndex];
    }
}
