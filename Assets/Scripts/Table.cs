using UnityEngine;
using UnityEngine.EventSystems;

public class Table : MonoBehaviour
{
    [SerializeField] private bool _fullPotionTable;
    private TableManager _tableManager;
    public bool FullPotionTable => _fullPotionTable;

    public Vector2 SetPositionForBottle()
    {
        if(transform.childCount == 0)
        {
            return transform.position;
        }
        else if(transform.childCount > 0 && _fullPotionTable)
        {
            return transform.position + new Vector3(transform.childCount, 0, 0);
        }
        else  ///clear table
        {            
            return transform.position - new Vector3(transform.childCount,0,0);
        }
    }
}
