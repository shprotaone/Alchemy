using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteSeries
{
    private float _standartSeies = 1;

    public float CurrentMultiply { get; private set; }

    public CompleteSeries()
    {
        CurrentMultiply = _standartSeies;
    }

    public void IncreaseMultiply(int matchIndex)
    {
        if (matchIndex == 1) CurrentMultiply += 0.05f;
        else if (matchIndex == 2) CurrentMultiply += 0.1f;
        else if (matchIndex == 3) CurrentMultiply += 0.2f;
    }

    public void ResetSeries()
    {
        CurrentMultiply = _standartSeies;
    }
}
