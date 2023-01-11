using UnityEngine;
using System.Collections;

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

    private void OnEnable()
    {
        if (_action is Ingredient)
        {
            _collider.enabled = false;
        }
    }

    public void StartDragAction()
    {
        _action.Action();   
    }

    public void DropAction()
    {
        _collider.enabled = true;

        StartCoroutine(DropActionWithDelay());
    }

    private IEnumerator DropActionWithDelay()
    {
        //_collider.enabled = true;      

        if (_action is Ingredient || _action is Bottle)
        {
            yield return new WaitForSeconds(0.1f);

            if (_collider != null)
            {
                _collider.enabled = false;
            }

            yield return new WaitForSeconds(0.1f);     //Как можно еще задержать исполнение? 

            _action.Drop();
        }

        yield return null;
    }
}
