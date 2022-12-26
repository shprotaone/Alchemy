using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour,IPooledObject
{
    public ObjectType Type => ObjectType.COINDROP;

    public void Movement(Vector3 jarPosition)
    {
        transform.DOMove(jarPosition, 1, false).SetEase(Ease.InOutBack, 0.5f).OnComplete(DestroyCoin);
    }
    private void DestroyCoin()
    {
        ObjectPool.SharedInstance.DestroyObject(this.gameObject);
    }
}
