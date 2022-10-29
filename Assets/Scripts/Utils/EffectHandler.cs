using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    [SerializeField] private GameObject _blinkedPrefab;
    [SerializeField] private GameObject _firePrefab;
    [SerializeField] private GameObject _smokePrefab;
    [SerializeField] private GameObject _sparkPrefab;

    public GameObject GetEffect(ResourceType type)
    {
        return type switch
        {
            ResourceType.Eye => _sparkPrefab,
            ResourceType.Ladan => _blinkedPrefab,
            ResourceType.Sand => _smokePrefab,
            ResourceType.Stone => _firePrefab,
            _ => null,
        };
    }

}
