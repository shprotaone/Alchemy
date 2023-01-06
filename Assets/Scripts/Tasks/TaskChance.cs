using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskChance: MonoBehaviour
{
    [SerializeField] private List<CounterTask> Chances;

    public int digit;
    public int totalWeight;

    private void Start()
    {
        foreach (var item in Chances)
        {
            totalWeight += item.weight;
        }
    }
    public int GetTaskCount()
    {
        digit = Random.Range(0, totalWeight);

            for (int i = 0; i < Chances.Count; i++)
            {
                if (Chances[i].weight >= digit)
                {
                    Debug.Log(Chances[i].count);
                    return Chances[i].count;
                }

                digit -= Chances[i].weight;
            }

        Debug.LogWarning("Шанс не рассчитался");
        return 1;
    }

    public PotionLabelType GetRandomLabel()
    {
        int result = UnityEngine.Random.Range(0, (int)PotionLabelType.FIRE + 1);

        return (PotionLabelType)result;
    }
}
