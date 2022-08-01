using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightObject : MonoBehaviour
{
    [SerializeField] private List<GameObject> _brightObjectInRoom;

    private string _interactiveLayerName = "Interractive";
    private string _dialogLayerName = "Dialog";

    public string InteractiveLayerName => _interactiveLayerName;
    public string DialogLayerName => _dialogLayerName;

    public void BrightObjects(bool flag)
    {
        List<SpriteRenderer> brightObject = new List<SpriteRenderer>();

        string nameLayer;
        if (flag)
        {
            nameLayer = _dialogLayerName;
        }
        else
        {
            nameLayer = _interactiveLayerName;
        }

        foreach (var item in _brightObjectInRoom)
        {
            brightObject.Add(item.GetComponentInChildren<SpriteRenderer>());
        }

        foreach (var item in brightObject)
        {
            item.sortingLayerName = nameLayer;
        }
    }
}
