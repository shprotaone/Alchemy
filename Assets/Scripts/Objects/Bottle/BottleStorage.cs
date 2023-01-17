using DG.Tweening;
using TMPro;
using UnityEngine;

public class BottleStorage : MonoBehaviour,IAction,IInterract,IDragTimer
{
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private TMP_Text _amountText; //разделить на view?    
    [SerializeField] private BottleInventory _bottleInventory;
    [SerializeField] private TableManager _tableManager;
    [SerializeField] private LabelToSprite _labelToSprite;
    [SerializeField] private Transform _uprisePos;
    [SerializeField] private ParticleSystem _upriseParticle;

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

    public BottleModel CreateBottle()
    {
        GameObject bottleGO = ObjectPool.SharedInstance.GetObject(ObjectType.BOTTLE);

        BottleModel bottle = bottleGO.GetComponent<BottleModel>();
        bottle.transform.position = _uprisePos.position;
        _upriseParticle.Play();

        bottle.InitBottle(this, _tableManager,_bottleInventory);
        _boxCollider.enabled = false;

        return bottle;
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
        //CreateBottle();
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
