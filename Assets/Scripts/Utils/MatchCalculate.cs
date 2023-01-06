using System.Collections.Generic;
using UnityEngine;

public class MatchCalculate
{  
    public int IndexMatchLabel(List<PotionLabelType> labelsInBottle, List<PotionLabelType> labelsInTask)
    {
        int matchIndex = 0;

        labelsInBottle.Sort();
        labelsInTask.Sort();

        List<LabelsPair> inTask = FillDictionaryTask(labelsInTask);

        for (int i = 0; i < labelsInBottle.Count; i++)
        {
            for (int j = 0; j < labelsInTask.Count; j++)
            {               
                if (inTask[j].Label == labelsInBottle[i] && !inTask[j].IsFound)
                {
                    inTask[j].SetFound();
                    matchIndex++;
                    break;
                }
            }           
        }
        
        return matchIndex;
    }

    private List<LabelsPair> FillDictionaryTask(List<PotionLabelType> labelsInTask)
    {
        List<LabelsPair> inTask = new List<LabelsPair>();

        for (int i = 0; i < labelsInTask.Count; i++)
        {
            inTask.Add(new LabelsPair(labelsInTask[i]));
        }

        return inTask;
    }
}

public class LabelsPair
{
    private PotionLabelType _label;
    private bool _isFound;

    public PotionLabelType Label => _label;
    public bool IsFound => _isFound;

    public LabelsPair(PotionLabelType label)
    {
        _label = label;
        _isFound = false;
    }

    public void SetFound()
    {
        _isFound = true;
    }
}
