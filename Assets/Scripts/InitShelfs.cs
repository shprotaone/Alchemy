using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitShelfs : MonoBehaviour
{
    [SerializeField] private GameObject _currentIngredientPrefab;
    [SerializeField] private Transform _parentDragableObject;

    private Shelf[] _shelfs;

    public GameObject CurrentPrefab => _currentIngredientPrefab;
    public Transform ParentDragableObject => _parentDragableObject;
    void Start()
    {
        _shelfs = GetComponentsInChildren<Shelf>();
        FillShelfs();
    }

    private void FillShelfs()
    {
        foreach (var item in _shelfs)
        {
            item.Init();
        }
    }
}
