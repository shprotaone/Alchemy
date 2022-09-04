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

    public void DraggableAction()
    {
        _action.Action();        
    }

    public void DropMovementAction()
    {
        _action.Movement();
       StartCoroutine(DelayDisableCollider());
    }

    private IEnumerator DelayDisableCollider()      //мешает при перетаскивании заново
    {
        yield return new WaitForFixedUpdate();
        _collider.enabled = false;

        yield return null;
    }
}
