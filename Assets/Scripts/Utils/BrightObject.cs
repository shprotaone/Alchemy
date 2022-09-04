using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightObject : MonoBehaviour
{
    [SerializeField] private List<GameObject> _brightObjectInRoom;
    [SerializeField] private List<SpriteRenderer> _visitors;

    private string _interactiveLayerName = "Interractive";
    private string _dialogLayerName = "Dialog";
    private string _brightLayerName = "BrightObjects";
    private string _UILayerName = "UI";

    public string InteractiveLayerName => _interactiveLayerName;
    public string DialogLayerName => _dialogLayerName;
    public string BrightLayerName => _brightLayerName;
    public string UILayerName => _UILayerName;

    /// <summary>
    /// Подсвечивание кнопок для туториала
    /// </summary>
    /// <param name="flag"></param>
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

    public void BrightVisitors(bool flag)
    {
        if (flag)
        {
            foreach (var item in _visitors)
            {
                item.sortingLayerName = _brightLayerName;
            }
        }
        else
        {
            foreach (var item in _visitors)
            {
                item.sortingLayerName = _interactiveLayerName;
            }
        }
    }
}
