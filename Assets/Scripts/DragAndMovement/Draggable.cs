using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public bool IsDragging;

    private Collider2D _collider;
    private DragController _dragController;
    private Bottle _currentBottle;
    private Ingredient _currentIngredient;

    public Bottle CurrentBottle => _currentBottle;
    public Ingredient CurrentIngredient => _currentIngredient;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _dragController = DragController.instance;

        if(TryGetComponent(out _currentBottle)) { }
        else if(TryGetComponent(out _currentIngredient)) { }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Draggable colliderDraggble = other.GetComponent<Draggable>();     //выталкивание, если объекты рядом

        //if (colliderDraggble != null && _dragController.LastDragged.gameObject == gameObject)
        //{
        //    ColliderDistance2D colliderDistance2D = other.Distance(_collider);
        //    Vector3 diff = new Vector3(colliderDistance2D.normal.x, colliderDistance2D.normal.y) * colliderDistance2D.distance; //??

        //    transform.position -= diff;
        //}
    }
}
