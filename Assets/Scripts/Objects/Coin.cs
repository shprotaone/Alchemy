using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    public void Movement(Vector3 jarPosition )
    {
        transform.DOMove(jarPosition, 1, false).SetEase(Ease.InOutBack, 0.5f).OnComplete(DestroyCoin);
    }
    private void DestroyCoin()
    {
        Destroy(this.gameObject);
    }
}
