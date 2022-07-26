using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ShopController : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private VisitorController _visitorController;
    

    private void Start()
    {
        GlobalTaskController.OnLevelComplete += ShopClose;           
    }

    private void ShopOpen()
    {
        _visitorController.ShopControl(true);
        transform.DORotate(new Vector3(0, 0, 0), 1);
    }

    private void ShopClose()
    {
        _visitorController.ShopControl(false);
        transform.DORotate(new Vector3(0, 180, 0), 1);
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

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(DelayShopControl());
    }

    private void OnDisable()
    {
        GlobalTaskController.OnLevelComplete -= ShopClose;
    }
}
