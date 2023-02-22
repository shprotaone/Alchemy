using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CookSubject : MonoBehaviour,ISubject
{
    [SerializeField] private PotionStock _stock;

    private AchievementData _commonBottleCounter;
    private AchievementData _fireLabelCounter;
    private AchievementData _waterLabelCounter;
    private AchievementData _rockLabelCounter;
    private AchievementData _potionStockCounter;

    public void Init(List<AchievementData> achievments)
    {
        foreach (var item in achievments)
        {
            if (item.Id == AchieveID.COMMONBOTTLE) _commonBottleCounter = item;
            else if (item.Id == AchieveID.FIRELABEL) _fireLabelCounter = item;
            else if (item.Id == AchieveID.WATERLABEL) _waterLabelCounter = item;
            else if (item.Id == AchieveID.ROCKLABEL) _rockLabelCounter = item;
            else if (item.Id == AchieveID.POTIONSTOCK) _potionStockCounter = item;
        }
    }

    public void AddCount()
    {
        _commonBottleCounter.Increase();
    }

    public void AddCountLabel(PotionLabelType type)
    {
        if (type == PotionLabelType.WATER) _waterLabelCounter.Increase();
        else if (type == PotionLabelType.FIRE) _fireLabelCounter.Increase();
        else if (type == PotionLabelType.ROCK) _rockLabelCounter.Increase();
    }

    public void CheckCookPotion(List<PotionLabelType> labels)
    {
        foreach (var item in _stock.PotionStockList)
        {
            labels.Sort();
            item.labels.Sort();
            if (!item.isCooked && item.labels.SequenceEqual(labels))
            {
                item.isCooked = true;
                _potionStockCounter.Increase();
                return;
            }
        }
    }
}
