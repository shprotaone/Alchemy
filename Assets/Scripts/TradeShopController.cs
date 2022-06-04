using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TradeShopController : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private VisitorController _visitorController;

    [SerializeField] private Image _image;

    private void Start()
    {
        if (!_visitorController.ShopIsOpen)
            _image.color = Color.red;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_visitorController.ShopIsOpen)
        {
            _visitorController.ShopIsOpen = false;
            _image.color = Color.red;
        }
        else
        {
            _visitorController.ShopIsOpen = true;
            _image.color = Color.white;
        }
    }
}
