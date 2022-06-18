using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DragController))]
public class ObjectCheckerInvoke : MonoBehaviour
{
    [SerializeField] private VisitorController _visitorController;

    private Slot _currentSlot;
    private ICommand _command;

    private void OnMouseDown()
    {
        if (gameObject.name == "Plate")
        {
            //_command = new TradeShopController(_visitorController, gameObject.transform.GetComponent<SpriteRenderer>());
            //_command.Execute();
        }
        else if(gameObject.transform.TryGetComponent(out _currentSlot))
        {
            _currentSlot.OnBeginDrag();
        }
    }
}
