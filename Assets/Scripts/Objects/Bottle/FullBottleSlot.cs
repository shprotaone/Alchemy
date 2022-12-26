using UnityEngine;

public class FullBottleSlot : MonoBehaviour
{
    public bool IsFree { get; private set; }
    public Potion PotionInSlot { get; private set; }
    public Vector3 SlotPosition { get; private set; }

    private void Start()
    {
        IsFree = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Bottle bottle))
        {
            bottle.SetPosition(this.transform);
            PotionInSlot = bottle.PotionInBottle;
            IsFree = false;
        }
    }
}
