using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{   
    private Camera _cam;

    [SerializeField] private Transform _orderWindow;
    [SerializeField] private Transform _room;
    [SerializeField] private Button _cameraMovementButton;
    [SerializeField] private RectTransform _buttonTransfrom;
    [SerializeField] private float _cameraSpeed;
    
    private Direction _currentDirection;

    public void Init()
    {
        _cam = Camera.main;

        _cameraMovementButton.onClick.AddListener(Movement);

        _currentDirection = Direction.Up;
        Movement();
    }

    public void Movement()
    {
        if (_currentDirection == Direction.Down)
        {
            _cam.transform.DOMove(_orderWindow.position, _cameraSpeed, false);
            _currentDirection = Direction.Up;
            _buttonTransfrom.DORotate(new Vector3(0, 0, 180), 1, RotateMode.Fast);
        }
        else
        {
            _cam.transform.DOMove(_room.position, _cameraSpeed, false);
            _currentDirection = Direction.Down;
            _buttonTransfrom.DORotate(new Vector3(0, 0, 0), 1, RotateMode.Fast);
        }
    }
}
