using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{   
    private Camera _cam;

    [SerializeField] private Transform _orderWindow;
    [SerializeField] private Transform _room;
    [SerializeField] private Button _button;
    [SerializeField] private RectTransform _buttonTransfrom;
    [SerializeField] private float _cameraSpeed;
    
    private SwipeDirection _currentDirection;

    private bool _startRoom;

    private void Start()
    {
        _cam = Camera.main;

        _button.onClick.AddListener(Movement);


        StartPosition();
        Movement();
    }

    public void Movement()
    {
        if (_currentDirection == SwipeDirection.Down)
        {
            _cam.transform.DOMove(_orderWindow.position, _cameraSpeed, false);
            _currentDirection = SwipeDirection.Up;
            _buttonTransfrom.DORotate(new Vector3(0, 0, 180), 1, RotateMode.Fast);
        }
        else
        {
            _cam.transform.DOMove(_room.position, _cameraSpeed, false);
            _currentDirection = SwipeDirection.Down;
            _buttonTransfrom.DORotate(new Vector3(0, 0, 0), 1, RotateMode.Fast);
        }
    }

    private void StartPosition()
    { 
        if (!_startRoom)
            _currentDirection = SwipeDirection.Down;
        else
            _currentDirection = SwipeDirection.Up;
    }

    public void SetStartPosition(bool startRoom)
    {
        _startRoom = startRoom;
    }
}
