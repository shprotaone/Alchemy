using UnityEngine;

public class MatchCalculate
{
    /// <summary>
    /// ������ ������ ���������� � ��������
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
        Debug.Log("������� " + matchIndex + " ������������");
        return matchIndex;
    }
}
