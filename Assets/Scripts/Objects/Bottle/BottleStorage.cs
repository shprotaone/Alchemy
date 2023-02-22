using UnityEngine;

public class BottleStorage : MonoBehaviour
{
    [SerializeField] private BottleInventory _bottleInventory;
    [SerializeField] private Transform _uprisePos;
    [SerializeField] private ParticleSystem _upriseParticle;

    private LabelToSprite _labelToSprite;

    public LabelToSprite LabelToSprite => _labelToSprite;

    public void InitBottleStorage(LabelToSprite labelToSprite)
    {
        _labelToSprite = labelToSprite;
    }

    public BottleModel CreateBottle()
    {
        bool created = false;
        BottleModel model = null;

        while (!created)
        {
            GameObject bottleGO = ObjectPool.SharedInstance.GetObject(ObjectType.BOTTLE);
            BottleModel bottle = bottleGO.GetComponent<BottleModel>();

            if (!bottle.IsFull)
            {
                bottle.transform.position = _uprisePos.position;
                _upriseParticle.Play();

                bottle.InitBottle(this, _bottleInventory);
                created = true;
                model = bottle;
            }
        }

        return model;
    }
}
