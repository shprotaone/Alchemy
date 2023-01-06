using System.Collections.Generic;
using UnityEngine;

public class DraggableObjectController : MonoBehaviour
{
    [SerializeField] private MenuPanelController[] _menuPanelController;

    [SerializeField] private GameObject[] _interractGo;
    [SerializeField] private List<IInterract> _interract;

    private void Awake()
    {
        _interract = new List<IInterract>();

        for (int i = 0; i < _menuPanelController.Length; i++)
        {
            _menuPanelController[i].OnInterract += SetInterract;
        }
        
        for (int i = 0; i < _interractGo.Length; i++)
        {
            _interractGo[i].TryGetComponent(out IInterract component); ;

            _interract.Add(component);
        }

        //SetInterract(true);
    }

    public void SetInterract(bool flag)
    {
        foreach (var item in _interract)
        {
            item.SetInterract(flag);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _menuPanelController.Length; i++)
        {
            _menuPanelController[i].OnInterract -= SetInterract;
        }
    }
}
