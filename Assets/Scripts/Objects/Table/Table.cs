using UnityEngine;
using UnityEngine.EventSystems;

public class Table : MonoBehaviour
{
    [SerializeField] private bool _fullPotionTable;
    private Bottle _bottle;
    public bool FullPotionTable => _fullPotionTable;

    public Vector2 SetStartPosition()
    {
        if (transform.childCount > 0 && _fullPotionTable)
        {
            return transform.position + new Vector3(transform.childCount, 0, 0);
        }
        else if (transform.childCount > 0 && !_fullPotionTable)
        {
            return transform.position - new Vector3(transform.childCount, 0, 0);
        }
        else
        {
            return transform.position;
        }
    }

    public Vector2 SetPositionForBottle()
    {
        _bottle = GetComponentInChildren<Bottle>();

        if (_bottle != null)
        {
            _bottle.transform.position = transform.position;

            if (_fullPotionTable)
            {
                return _bottle.transform.position + new Vector3(1, 0, 0);
            }
            else
            {
                return _bottle.transform.position - new Vector3(1, 0, 0);
            }
        }
        else return transform.position;
    }
}
