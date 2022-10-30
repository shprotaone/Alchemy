using System.Collections;
using System.Collections.Generic;
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
    }

    public IEnumerator DropAction()
    {
        yield return new WaitForEndOfFrame();       //Как можно еще задержать исполнение? 
        
        if(_action is Ingredient)
        {           
            _action.Movement();          
        }

        yield return new WaitForSeconds(0.1f);

        _collider.enabled = false;

        yield break;
    }
}
