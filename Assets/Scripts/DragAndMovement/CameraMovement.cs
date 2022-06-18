using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour
{
    private Camera _cam;

    [SerializeField] private Transform _orderWindow;
    [SerializeField] private Transform _room;
    [SerializeField] private Transform _button;
    [SerializeField] private float _cameraSpeed;
    [SerializeField] private bool _startRoom;

    private DragController _dragController;
    private SwipeDirection _currentDirection;
    private Vector3 _velocity = Vector3.zero;
    
    private void Start()
    {
        _cam = Camera.main;
        _dragController = DragController.instance;

        StartPosition();
    }

    private void OnMouseDown()
    {
        Movement();
    }

    private void Movement()
    {
        if (_currentDirection == SwipeDirection.Down)
        {
            _cam.transform.DOMove(_orderWindow.position, _cameraSpeed, false);
            _currentDirection = SwipeDirection.Up;
            _button.DORotate(new Vector3(0, 0, 180), 1, RotateMode.Fast);
        }
        else
        {
            _cam.transform.DOMove(_room.position, _cameraSpeed, false);
            _currentDirection = SwipeDirection.Down;
            _button.DORotate(new Vector3(0, 0, 0), 1, RotateMode.Fast);
        }
    }

    private void StartPosition()
    { 
        if (!_startRoom)
            _currentDirection = SwipeDirection.Down;
        else
            _currentDirection = SwipeDirection.Up;
    }
}
