using System.Collections.Generic;
using UnityEngine;
using YG;

public class PotionStock : MonoBehaviour
{
    private string _fileName = "PotionStockProgress.json";
    private string _SOName = "PotionForStock";

    [SerializeField] private List<PotionForStock> _potionStockList;

    public List<PotionForStock> PotionStockList => _potionStockList;
    private SaveToString<PotionForStock> _strings;
    //private JSONSave<PotionForStock> JSONSave;

    private void Start()
    {
        _strings = new SaveToString<PotionForStock>(_SOName);

        List<PotionForStock> list = new List<PotionForStock>();
        list = _strings.LoadFromStrings(YandexGame.savesData.openPotions);

        if (list[0].labels == null)
        {
            Save();
        }
        else
        {
            SetLoadData(list);
        }

        LevelInitializator.OnLevelStarted += Save;

    }

    private void SetLoadData(List<PotionForStock> potion)
    {
        for (int i = 0; i < _potionStockList.Count; i++)
        {
            _potionStockList[i].isCooked = potion[i].isCooked;
        }
    }

    private void Save()
    {
        YandexGame.savesData.openPotions = _strings.SaveToList(_potionStockList);
        YandexGame.SaveProgress();
    }

    private void OnDisable()
    {
        LevelInitializator.OnLevelStarted -= Save;
        Save();
    }
}


