using UnityEngine;
using DG.Tweening;

public class LibraVisual : MonoBehaviour
{
    private const float angleRot = 7;

    [SerializeField] private Transform _longTransform;
    [SerializeField] private Transform _leftSprite;
    [SerializeField] private Transform _rightSprite;

    [SerializeField] private bool _left;
    [SerializeField] private bool _right;
    [SerializeField] private bool _center;


    public void SetPosition(int labelTaskInBottle, int labelCountTask)
    {
        if (labelTaskInBottle == labelCountTask)
        {
            ResetPos();
        } 
        else if (labelTaskInBottle < labelCountTask)
        {
            LeftPos();
        }
        else if(labelTaskInBottle > labelCountTask)
        {
            RightPos();
        }
    }


    public void LeftPos()
    {
        _longTransform.DORotate(Vector3.forward * angleRot, 1f);
        _leftSprite.DOLocalRotate(Vector3.forward * -angleRot, 1f);
        _rightSprite.DOLocalRotate(Vector3.forward * -angleRot, 1f);
    }

    public void RightPos()
    {
        _longTransform.DORotate(Vector3.forward * -angleRot, 1f);
        _leftSprite.DOLocalRotate(Vector3.forward * angleRot, 1f);
        _rightSprite.DOLocalRotate(Vector3.forward * angleRot, 1f);
    }

    public void ResetPos()
    {
        _longTransform.DORotate(Vector3.zero, 1f);
        _leftSprite.DORotate(Vector3.zero, 1f);
        _rightSprite.DORotate(Vector3.zero, 1f);
    }
}
