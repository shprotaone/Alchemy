using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ShopController : MonoBehaviour,IPointerClickHandler,IInterract
{
    [SerializeField] private VisitorController _visitorController;
    [SerializeField] private Transform _plate;

    private bool _isWorked;
    private bool _interract;

    public Transform Plate => _plate;
    private void OnEnable()
    {
        UniversalGlobalTask.OnLevelComplete += ShopClose;           
    }

    private void ShopOpen()
    {
        _visitorController.ShopControl(true);
        _plate.transform.DORotate(new Vector3(0, 0, 0), 1);
    }

    private void ShopClose()
    {
        _visitorController.ShopControl(false);
        _plate.transform.DORotate(new Vector3(0, 180, 0), 1);
    }

    private IEnumerator DelayShopControl()
    {
        yield return new WaitForSeconds(0.5f);
        if (_visitorController.ShopIsOpen)
            ShopClose();
        else
            ShopOpen();
        
        StopAllCoroutines();
    }

    public void SetDraggable(bool value)
    {
        _isWorked = value;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_interract)
        {
            StartCoroutine(DelayShopControl());
        }           
    }

    private void OnDisable()
    {
        UniversalGlobalTask.OnLevelComplete -= ShopClose;
    }

    public void SetInterract(bool value)
    {
        _interract = value;
    }
}
