using UnityEngine;

public class TradeShopController : MonoBehaviour
{
    [SerializeField] private VisitorController _visitorController;
    private SpriteRenderer _sprite;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        Controller();
    }

    private void OnMouseDown()
    {
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
}
