using TMPro;
using UnityEngine;

public class BottleStorage : MonoBehaviour,IAction,IInterract,IDragTimer
{
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private TMP_Text _amountText; //разделить на view?    
    [SerializeField] private ContrabandPotionSystem _contrabandPotionSystem;
    [SerializeField] private TableManager _tableManager;
    [SerializeField] private LabelToSprite _labelToSprite;

    [SerializeField] private int _delayDrag;

    private LocalTimer _timer;
    private int _amount;
    private bool _interract;

    public LabelToSprite LabelToSprite => _labelToSprite;

    public void InitBottleStorage(int bottleCount)
    {
        _amount = bottleCount;
        RefreshAmount();

        InitTimer(_delayDrag);
    }

    private void StartDrag()
    {
        if (_amount > 0 && !_timer.Started && _interract)
        {
            StartTimer();
            DecreaseAmount();

            GameObject bottleGO = ObjectPool.SharedInstance.GetObject(ObjectType.BOTTLE);
            bottleGO.transform.SetParent(this.transform);
            Bottle bottle = bottleGO.GetComponent<Bottle>();

            bottle.InitBottle(this,_tableManager);
            _boxCollider.enabled = false;
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

    public void ReturnBottle()
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

    public void Drop()
    {
        
    }

    public void SetInterract(bool value)
    {
        _interract = value;
        _boxCollider.enabled = true;
    }

    public void InitTimer(int delayDrag)
    {
        _timer = new LocalTimer(delayDrag, false);
        _timer.OnTimerEnded += SetInterract;
    }

    public void StartTimer()
    {
        StartCoroutine(_timer.StartTimer());
    }
}
