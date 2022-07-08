using UnityEngine;
using DG.Tweening;
using System;

public class CameraMovement : MonoBehaviour
{   
    private Camera _cam;

    [SerializeField] private Transform _orderWindow;
    [SerializeField] private Transform _room;
    [SerializeField] private Transform _button;
    [SerializeField] private float _cameraSpeed;
    
    private SwipeDirection _currentDirection;
    private NextCountHandler _nextDialog;

    private bool _startRoom;
    private bool _isFirstChangePos;

    private void Start()
    {
        _cam = Camera.main;
        StartPosition();
        Movement();

        _nextDialog = GetComponent<NextCountHandler>();
    }

    private void OnMouseDown()
    {
        Movement(); //ףיעט מע Mouse
        if (_isFirstChangePos)
        {
            _nextDialog.DisableClickHerePrefab();
            _isFirstChangePos = false;
        }
        
    }

    public void Movement()
    {
        if (_currentDirection == SwipeDirection.Down)
        {
            _cam.transform.DOMove(_orderWindow.position, _cameraSpeed, false);
            _currentDirection = SwipeDirection.Up;
            _button.DORotate(new Vector3(0, 0, 180), 1, RotateMode.Fast);

            _isFirstChangePos = true;
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

    public void SetStartPosition(bool startRoom)
    {
        _startRoom = startRoom;
    }
}
