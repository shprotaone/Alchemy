using System.Collections.Generic;
using UnityEngine;

public class PotionStock : MonoBehaviour
{
    private string _fileName = "PotionStockProgress.json";
    private string _SOName = "PotionForStock";

    [SerializeField] private List<PotionForStock> _potionStockList;

    public List<PotionForStock> PotionStockList => _potionStockList;
    private JSONSave<PotionForStock> JSONSave;

    private void Start()
    {
        JSONSave = new JSONSave<PotionForStock>(_fileName, _SOName);

        List<PotionForStock> list = new List<PotionForStock>();
        list = JSONSave.LoadFromJson();

        if(list.Count == 0)
        {
            JSONSave.SaveToJson(_potionStockList);
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
        JSONSave.SaveToJson(_potionStockList);
    }

    private void OnDisable()
    {
        LevelInitializator.OnLevelStarted -= Save;
        Save();
    }
}


