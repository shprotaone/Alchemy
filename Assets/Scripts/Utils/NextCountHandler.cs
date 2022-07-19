using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NextCountHandler : MonoBehaviour
{
    /// <summary>
    /// Отключает clickHere и переключает на следующий count
    /// </summary>
    public static Action OnNextCount;

    [SerializeField] private GameObject _clickHerePrefab;

    public void DisableClickHerePrefab()
    {
        if(_clickHerePrefab != null && _clickHerePrefab.activeInHierarchy)
        {
            _clickHerePrefab.SetActive(false);
            Destroy(this);
        }        

        OnNextCount?.Invoke();
    }
}
