using UnityEngine;

public class FullBottleSlot : MonoBehaviour
{
    private Bottle _bottleInSlot;
    public bool IsFree { get; private set; }
    public Potion PotionInSlot { get; private set; }
    public Vector3 SlotPosition { get; private set; }

    private void Start()
    {
        IsFree = true;
        PotionTaskSystem.OnTaskComplete += Reset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Bottle bottle) && IsFree)
        {
            bottle.OnDropped += SetFreeSlot;
            bottle.SetPosition(this.transform);
            PotionInSlot = bottle.PotionInBottle;
            IsFree = false;
            _bottleInSlot = bottle;
        }
    }

    private void SetFreeSlot()
    {
        IsFree = true;
        if(_bottleInSlot != null)
        {
            _bottleInSlot.OnDropped -= SetFreeSlot;
        }
   
    }

    private void Reset()
    {
        SetFreeSlot();
        PotionTaskSystem.OnTaskComplete -= Reset;
    }
}
