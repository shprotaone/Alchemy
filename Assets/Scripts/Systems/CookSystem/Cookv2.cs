using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Cookv2 : MonoBehaviour
{
    [SerializeField] private BottleStorage _bottleStorage;
    [SerializeField] private ClickController _clickController;
    [SerializeField] private MixingSystemv3 _mixingSystem;
    [SerializeField] private ClaudronSystem _claudron;
    [SerializeField] private CookSubject _cookSubject;

    private bool _canFillBottle = false;
    
    public bool CanFillBottle => _canFillBottle;

    private void Start()
    {
        _clickController.OnGoodPotion += CookComplete;
        _clickController.OnBadPotion += CookFailed;
    }

    private void CookComplete()
    {
        if(_mixingSystem.IngredientsInClaudron.Count != 0)
        {
            _canFillBottle = true;
            BottleModel bottle = _bottleStorage.CreateBottle();

            FillCommonSubject();
            FillLabelSubjects(_mixingSystem.LabelSetter.Labels);

            _mixingSystem.FillBottle(bottle);
            //TODO Создание бутылки           
        }
    }

    private void CookFailed()
    {
        _claudron.CrunchClaudron(true);
        DOVirtual.DelayedCall(3, FillBottleReset);
    }

    public void FillBottleReset()
    {
            _canFillBottle = false;
            _claudron.CrunchClaudron(false);
            //_clickController.Reset();        
    }

    private void FillLabelSubjects(List<PotionLabelType> labels)
    {
        foreach (var item in labels)
        {
            _cookSubject.AddCountLabel(item);
        }

        _cookSubject.CheckCookPotion(labels);
        
    }
    private void FillCommonSubject()
    {
        _cookSubject.AddCount();
    }

    private void OnDisable()
    {
        _clickController.OnGoodPotion -= CookComplete;
        _clickController.OnBadPotion -= CookFailed;
    }
}
