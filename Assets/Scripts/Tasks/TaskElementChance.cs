using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskElementChance
{
    private List<RandomPart> _chancesElementList;

    private int digit;
    private int totalWeight;

    public int MaxWeightIndex { get; private set; }

    public TaskElementChance(List<RandomPart> chances)
    {
        _chancesElementList = chances;

        foreach (var item in _chancesElementList)
        {
            totalWeight += item.Weight;
        }

        SetMaxWeight();
    }

    public int GetLabelIndex()
    {
        digit = Random.Range(0, totalWeight);

        for (int i = 0; i < _chancesElementList.Count; i++)
        {
            if (_chancesElementList[i].Weight >= digit)
            {

                return i;
            }

            digit -= _chancesElementList[i].Weight;
        }

        return 1;
    }


    private void SetMaxWeight()
    {
        MaxWeightIndex = _chancesElementList.IndexOf(_chancesElementList.Max());
        Debug.Log(MaxWeightIndex);
    }
}
