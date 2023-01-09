using System.Collections.Generic;
using UnityEngine;

public class TaskChance
{
    private List<CounterTask> _chances;

    private int digit;
    private int totalWeight;

    public TaskChance (List<CounterTask> chances)
    {
        _chances = chances;

        foreach (var item in _chances)
        {
            totalWeight += item.Weight;
        }
    }

    public int GetTaskCount()
    {
        digit = Random.Range(0, totalWeight);

            for (int i = 0; i < _chances.Count; i++)
            {
                if (_chances[i].Weight >= digit)
                {
                    
                    return _chances[i].Count;
                }

                digit -= _chances[i].Weight;
            }

        return 1;
    }

    public PotionLabelType GetRandomLabel()
    {
        int result = UnityEngine.Random.Range(0, (int)PotionLabelType.FIRE + 1);

        return (PotionLabelType)result;
    }
}
