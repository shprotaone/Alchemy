using UnityEngine;

public class MatchCalculate
{
    /// <summary>
    /// Выдает индекс совпадения с заданием
    /// </summary>
    /// <param name="inBottle"></param>
    /// <param name="inTask"></param>
    /// <returns></returns>
   public static int IndexMatch(Potion inBottle,Potion inTask)
    {
        int matchIndex = 0;

        inBottle.Ingredients.Sort();
        inTask.Ingredients.Sort();

        for (int i = 0; i < inTask.Ingredients.Count; i++)
        {
            for (int j = 0; j < inBottle.Ingredients.Count; j++)
            {
                if (inBottle.Ingredients[j] == inTask.Ingredients[i])
                {
                    matchIndex++;
                }
            }

        }
        Debug.Log("Совпало " + matchIndex + " ингредиентов");
        return matchIndex;
    }

    public static int IndexMatchLabel(Potion inBottle, Potion inTask)
    {
        int matchIndex = 0;

        inBottle.Labels.Sort();
        inTask.Labels.Sort();

        if(inBottle.Labels.Count > inTask.Labels.Count)
        {
            for (int i = 0; i < inTask.Labels.Count; i++)
            {
                if (inBottle.Labels[i] == inTask.Labels[i])
                {
                    matchIndex++;
                }
            }
        }
        else
        {
            for (int i = 0; i < inBottle.Labels.Count; i++)
            {
                if (inBottle.Labels[i] == inTask.Labels[i])
                {
                    matchIndex++;
                }
            }
        }     
        
        return matchIndex;
    }
}
