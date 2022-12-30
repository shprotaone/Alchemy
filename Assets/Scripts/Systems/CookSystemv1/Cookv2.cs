using UnityEngine;

public class Cookv2 : MonoBehaviour
{
    [SerializeField] private ClickController _clickController;
    [SerializeField] private MixingSystemv3 _mixingSystem;
    [SerializeField] private ClaudronSystem _claudron;

    private bool _canFillBottle = false;

    public bool CanFillBottle => _canFillBottle;

    private void Start()
    {
        _clickController.OnGoodPotion += CookComplete;
        _clickController.OnNormalPotion += CookComplete;
        _clickController.OnBadPotion += CookFailed;
        //_clickController.OnResetClaudron += FillBottleReset;
    }

    private void CookComplete()
    {
        _canFillBottle = true;
        _mixingSystem.FillPotion();
    }

    private void CookFailed()
    {
        _claudron.CrunchClaudron(true);
    }

    public void FillBottleReset()
    {
        _canFillBottle = false;
        _clickController.Reset();
    }

    private void OnDisable()
    {
        _clickController.OnGoodPotion -= CookComplete;
        _clickController.OnNormalPotion -= CookComplete;
        _clickController.OnBadPotion -= CookFailed;
        //_clickController.OnResetClaudron -= FillBottleReset;
    }
}
