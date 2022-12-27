using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionTaskSystemv2 : MonoBehaviour
{
    [SerializeField] private VisitorController _visitorController;
    [SerializeField] private LabelToSprite _labelToSprite;

    private PotionTaskv2 _currentTask;
    private List<PotionLabelType> _labels;
    private Money _money;

    public void Init(Money money)
    {
        _money = money;
    }

    public PotionTaskv2 GetTask()
    {
        int count = Random.Range(1, 3);

        _labels = new List<PotionLabelType>();


        int result = Random.Range(0, (int)PotionLabelType.FIRE);
        
        for (int i = 0; i < count; i++)
        {
            _labels.Add((PotionLabelType)result);
        }

        _currentTask = new PotionTaskv2(_labels, GetReward(count), GetLabels(count));

        return _currentTask;
    }

    private int GetReward(int count)
    {
        if (count == 1) return 100;
        else if (count == 2) return 200;
        else if (count == 3) return 300;
        else
        {
            Debug.LogError("Значков нет");
            return 0;
        }
    }

    public void TaskComplete(PotionTaskv2 potionTask)
    {

    }

    public void TaskCanceled()
    {

    }

    public Sprite[] GetLabels(int count)
    {
        Sprite[] sprites = new Sprite[count];

        for (int i = 0; i < count; i++)
        {
            sprites[i] = _labelToSprite.GetSprite(_labels[i]);
        }

        return sprites;
    }
}
