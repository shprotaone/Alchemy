using System.Collections.Generic;
using UnityEngine;

public class TaskLabelCountChance
{
    private TaskElementChance _taskElementChances;
    private List<RandomPart> _chancesList;

    private int digit;
    private int totalWeight;


    public TaskLabelCountChance (List<RandomPart> countChances,List<RandomPart> labelChances)
    {
        _chancesList = countChances;

        foreach (var item in _chancesList)
        {
            totalWeight += item.Weight;
        }

        _taskElementChances = new TaskElementChance(labelChances);
    }

    public int GetLabelIndex()
    {
        digit = Random.Range(0, totalWeight);

            for (int i = 0; i < _chancesList.Count; i++)
            {
                if (_chancesList[i].Weight >= digit)
                {
                    
                    return _chancesList[i].Count;
                }

                digit -= _chancesList[i].Weight;
            }

        return 1;
    }

    public PotionLabelType GetLabel()
    {
        int result = _taskElementChances.GetLabelIndex();

        return (PotionLabelType)result;
    }

    public PotionLabelType GetLabelWithMaxWeight()
    {
        return (PotionLabelType)_taskElementChances.MaxWeightIndex;     //не лучшее решение, при изменении порядка enum скрипт будет работать неправильно 
    }
}
