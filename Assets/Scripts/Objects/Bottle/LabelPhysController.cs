using DG.Tweening;
using UnityEngine;

public class LabelPhysController : MonoBehaviour
{
    private Vector3 stablePos = new Vector3(1, 1.17f, 0);
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private Rigidbody2D _rigid;

    public void Activate()
    {
        _collider.enabled = true;
        _rigid.bodyType = RigidbodyType2D.Dynamic;
    }

    public void Deactivate()
    {
        _collider.enabled = false;
        _rigid.bodyType = RigidbodyType2D.Kinematic;
        DOVirtual.DelayedCall(1, () =>
        {
            transform.DOLocalMove(stablePos, 0.5f);
            transform.DOLocalRotate(Vector3.zero, 0.5f);
        });                      
    }
}