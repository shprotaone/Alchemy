using UnityEngine;
using UnityEngine.EventSystems;

public class Table : MonoBehaviour
{
    [SerializeField] private bool _fullPotionTable;
    private Bottle bottle;
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
        bottle = GetComponentInChildren<Bottle>();

        if (bottle != null)
        {
            bottle.transform.position = transform.position;

            if (_fullPotionTable)
            {
                return bottle.transform.position + new Vector3(1, 0, 0);
            }
            else
            {
                return bottle.transform.position - new Vector3(1, 0, 0);
            }
        }
        return transform.position;
    }
}
