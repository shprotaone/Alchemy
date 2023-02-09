using DG.Tweening;
using TMPro;
using UnityEngine;

public class BottleStorage : MonoBehaviour,IInterract,IDragTimer
{
    [SerializeField] private BottleInventory _bottleInventory;
    [SerializeField] private TableManager _tableManager;
    
    [SerializeField] private Transform _uprisePos;
    [SerializeField] private ParticleSystem _upriseParticle;

    [SerializeField] private int _delayDrag;

    private LabelToSprite _labelToSprite;
    private LocalTimer _timer;
    private int _amount;
    private bool _interract;

    public LabelToSprite LabelToSprite => _labelToSprite;

    public void InitBottleStorage(int bottleCount,LabelToSprite labelToSprite)
    {
        _amount = bottleCount;
        _labelToSprite = labelToSprite;

        InitTimer(_delayDrag);
    }

    public BottleModel CreateBottle()
    {
        GameObject bottleGO = ObjectPool.SharedInstance.GetObject(ObjectType.BOTTLE);

        BottleModel bottle = bottleGO.GetComponent<BottleModel>();
        bottle.transform.position = _uprisePos.position;
        _upriseParticle.Play();

        bottle.InitBottle(this,_bottleInventory);

        return bottle;
    }

    public void AddBottle(int value)
    {
        _amount += value;
    }

    public void SetInterract(bool value)
    {
        _interract = value;
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
