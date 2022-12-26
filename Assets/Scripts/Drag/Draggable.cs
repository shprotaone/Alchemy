using System.Threading.Tasks;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    [SerializeField] private bool _draggingObject;
    [SerializeField] private Collider2D _collider;
    private IAction _action;

    public bool DraggingObject => _draggingObject;
    public bool IsDragging { get; set; }

    private void Start()
    {
        _action = GetComponent<IAction>();
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

            _action.Drop();
        }
    }
}
