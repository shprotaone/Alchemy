using TMPro;
using UnityEngine;

public class BottleStorage : MonoBehaviour,IAction,IInterract
{
    private const int standartBottleCount = 20;

    [SerializeField] private Transform _parentBottle;
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private TMP_Text _amountText;
    [SerializeField] private FloatTimer _timer;
    [SerializeField] private ContrabandPotionSystem _contrabandPotionSystem;

    [SerializeField] private GameObject _bottlePrefab;
    [SerializeField] private Table _table;

    [SerializeField] private float _delayDrag;

    private int _amount;
    private bool _interract;

    public void InitBottleStorage(int bottleCount)
    {
        _amount = bottleCount;
        RefreshAmount();
    }

    private void StartDrag()
    {
        if (_amount > 0 && !_timer.TimerIsRunning && _interract)
        {
            _timer.InitTimer(_delayDrag);
            DecreaseAmount();
            GameObject bottleGO = Instantiate(_bottlePrefab,this.transform); //переделать на Pool
            Bottle bottle = bottleGO.GetComponent<Bottle>();

            bottle.InitBottle(this, _contrabandPotionSystem.ContrabandTimer);          
        }
    }

    public void AddBottle(int value)
    {
        _amount += value;
        RefreshAmount();
    }

    private void DecreaseAmount()
    {
        _amount--;
        RefreshAmount();
    }

    public void IncreaseAmount()
    {
        _amount++;
        _boxCollider.enabled = true;

        RefreshAmount();
    }

    private void RefreshAmount()
    {
        _amountText.text = _amount.ToString();
    }

    public void Action()
    {
        StartDrag();
    }

    public void Movement()
    {
        
    }

    public void SetInterract(bool value)
    {
        _interract = value;
    }
}
