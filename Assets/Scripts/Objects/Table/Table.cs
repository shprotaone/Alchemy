using UnityEngine;
using UnityEngine.EventSystems;

public class Table : MonoBehaviour
{
    [SerializeField] private bool _fullPotionTable;
    public bool FullPotionTable => _fullPotionTable;


    public Vector2 SetPositionForBottle()
    {
        if (transform.childCount > 0 && _fullPotionTable)
        {
            return transform.position + new Vector3(transform.childCount, 0, 0);
        }
        else if(transform.childCount > 0 && !_fullPotionTable)
        {
            return transform.position - new Vector3(transform.childCount, 0, 0);
        }
        else
        {
            print("Other");
            return transform.position;
        }
    }
}
