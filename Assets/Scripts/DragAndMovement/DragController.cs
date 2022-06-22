using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    public static DragController instance = null;

    [SerializeField] private Transform _tempTransform;

    private Camera _camera;
    private Vector2 _screenPosition;
    private Vector3 _worldPosition;
    private Draggable _lastDragged;

    private bool _isDragActive = false;
    private bool _isCall = false;

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
            if (Input.GetMouseButtonUp(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended))
            {
                Drop();
                return;
            }
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            _screenPosition = new Vector2(mousePos.x, mousePos.y);
        }
        else if (Input.touchCount > 0)
        {
            _screenPosition = Input.GetTouch(0).position;
        }
        else
        {
            return;
        }

        _worldPosition = _camera.ScreenToWorldPoint(_screenPosition);

        if (_isDragActive)
        {
            Drag();
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(_worldPosition, Vector2.zero);

            if (hit.collider != null)
            {
                Draggable draggable = hit.transform.gameObject.GetComponent<Draggable>();
                Slot slot = hit.transform.gameObject.GetComponent<Slot>();

                if (draggable != null && !_isDragActive)
                {
                    _lastDragged = draggable;
                    InitDrag();
                }
                else if (slot != null && !_isDragActive)
                {
                    if (!_isCall)
                    {
                        slot.OnBeginDrag();
                        InitDrag();
                        _isCall = true;
                    }

                }
            }
        }
        //SetDraggable();

    }

    private void CheckDrop()
    {
        if (_isDragActive)
        {
            if (Input.GetMouseButtonUp(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended))
            {
                Drop();
                return;
            }
        }
    }

    private void SetDraggable()
    {
        if (_isDragActive)
        {
            Drag();
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(_worldPosition, Vector2.zero);
            
            if(hit.collider != null)
            {
                Draggable draggable = hit.transform.gameObject.GetComponent<Draggable>();
                Slot slot = hit.transform.gameObject.GetComponent<Slot>();

                if(draggable != null && !_isDragActive)
                {
                    _lastDragged = draggable;
                    InitDrag();
                }
                else if(slot != null && !_isDragActive)
                {
                    if (!_isCall)
                    {
                        slot.OnBeginDrag();
                        InitDrag();
                        _isCall = true;
                    }
                    
                }
            }
        }

    }

    private void InitDrag()
    {
        UpdateDragStatus(true);
        if(_lastDragged != null && _lastDragged.CurrentBottle)
        _lastDragged.CurrentBottle.gameObject.transform.SetParent(_tempTransform);
    }

    private void Drag()
    {
        _lastDragged.transform.position = new Vector2(_worldPosition.x, _worldPosition.y);
    }

    private void Drop()
    {
        UpdateDragStatus(false);
        DropMovement();
        _isCall = false;
    }

    private void UpdateDragStatus(bool isDragging)
    {
        if(_lastDragged != null)
        {
            _isDragActive = _lastDragged.IsDragging = isDragging;
            _lastDragged.gameObject.layer = isDragging ? Layer.Dragging : Layer.Default;
        }
    }
    
    private void DropMovement()
    {
        if (_lastDragged.CurrentBottle != null)
        {
            _lastDragged.CurrentBottle.SetTable();            
        }
        else if (_lastDragged.CurrentIngredient != null)
        {
            _lastDragged.CurrentIngredient.Movement();
        }

        _lastDragged = null;
    }
}
