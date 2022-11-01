using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    [SerializeField] private bool _draggingObject;
    public bool IsDragging;

    private Collider2D _collider;
    private DragController _dragController;
    private IAction _action;

    public bool DraggingObject => _draggingObject;


    private void Start()
    {
        _action = GetComponent<IAction>();
        _collider = GetComponent<Collider2D>();        
    }

    public void StartDragAction()
    {
        _action.Action();

        if(_action is Ingredient)
        {
            _collider.enabled = false;
        }      
    }

    public async void DropAction()
    {
        _collider.enabled = true;

        if(_action is Ingredient || _action is Bottle)
        {
            await Task.Delay(30);

            if (_collider != null)
            {
                _collider.enabled = false;
            }

            await Task.Delay(50);      //Как можно еще задержать исполнение? 

            _action.Movement();
        }
    }
}
