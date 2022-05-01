using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bottle : MonoBehaviour
{
    [SerializeField] private Image _fullBottle;

    private bool _isFull;

    public bool IsFull => _isFull;

    public void FillBottle(Color color)
    {
        _fullBottle.enabled = true;
        _fullBottle.color = color;
        _isFull = true;
    }

}
