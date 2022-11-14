using System.Collections;
using UnityEngine;

public class DragController : MonoBehaviour
{
    public static DragController instance = null;

    [SerializeField] private Transform _draggableParent;

    private Camera _camera;
    private Vector2 _dragScreenPosition;
    private Vector3 _worldPosition;
    private Draggable _lastDragged;

    private bool _isDragActive = false;
    private bool _interractive = true;

    public Draggable LastDragged => _lastDragged;
    public bool IsDragActive => _isDragActive;
    public Vector3 WorldPosition => _worldPosition;

    private void Awake()
    {
        _camera = Camera.main;

        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (_isDragActive)
        {

            if (Input.GetMouseButtonUp(0))
            {
                Drop();
                return;
            }

            if ((Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended))
            {
                Drop();
                return;
            }
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            _dragScreenPosition = new Vector2(mousePos.x, mousePos.y);
        }
        else if (Input.touchCount > 0)
        {
            _dragScreenPosition = Input.GetTouch(0).position;
        }
        else
        {
            return;
        }

        _worldPosition = _camera.ScreenToWorldPoint(_dragScreenPosition);        

        if (_isDragActive)
        {
            Drag();
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(_worldPosition, Vector2.zero);

            if (hit.collider != null && _interractive)
            {
                Draggable draggable = hit.transform.GetComponent<Draggable>();

                Ingredient ingredient;
                Bottle bottle;

                if (draggable != null && !_isDragActive)
                {
                    _lastDragged = draggable;
                        
                    InitDrag();               

                    if (ingredient = draggable.GetComponentInChildren<Ingredient>())
                    {
                        _lastDragged = ingredient.GetComponent<Draggable>();
                    }
                    else if (bottle = draggable.GetComponentInChildren<Bottle>())
                    {
                        _lastDragged = bottle.GetComponent<Draggable>();
                    }
                }
            }
        }
    }

    public void InitDrag()
    {
        Handheld.Vibrate();
        UpdateDragStatus(true);
        _lastDragged.StartDragAction();
    }

    private void Drag()
    {
        if (_lastDragged.DraggingObject)
            _lastDragged.transform.position = new Vector2(_worldPosition.x, _worldPosition.y);
    }

    private void Drop()
    {
        UpdateDragStatus(false);

        _lastDragged.DropAction();
        
    }

    private void UpdateDragStatus(bool isDragging)
    {
        //if (_lastDragged != null)
        //{
            _isDragActive = _lastDragged.IsDragging = isDragging;
            _lastDragged.gameObject.layer = isDragging ? Layer.Dragging : Layer.Default;
        //}
    }

    /// <summary>
    /// Активация на перетаскивание
    /// </summary>
    /// <param name="flag"></param>
    public void ObjectsInterractable(bool flag)
    {
        _interractive = flag;
    }

}
