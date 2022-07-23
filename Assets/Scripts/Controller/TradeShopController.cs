using UnityEngine;
using UnityEngine.EventSystems;

public class TradeShopController : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private VisitorController _visitorController;
    private SpriteRenderer _sprite;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();

        GlobalTaskController.OnLevelComplete += Controller;
        Controller();
    }

    private void Controller()
    {
        if (_visitorController.ShopIsOpen)
        {
            _visitorController.ShopIsOpen = false;
            _sprite.color = Color.red;
        }
        else
        {
            _visitorController.ShopIsOpen = true;
            _sprite.color = Color.white;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Controller();
    }

    private void OnDisable()
    {
        GlobalTaskController.OnLevelComplete -= Controller;
    }
}
