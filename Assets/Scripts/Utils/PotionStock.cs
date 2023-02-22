using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class PotionStock : MonoBehaviour
{
    //private string _fileName = "PotionStockProgress.json"; для сохранения на андроид
    private string _SOName = "PotionForStock";

    [SerializeField] private List<PotionForStock> _potionStockList;

    public List<PotionForStock> PotionStockList => _potionStockList;
    private SaveToString<PotionForStock> _strings;

    private string[] _load;
    //private JSONSave<PotionForStock> JSONSave;

    private void OnEnable() => YandexGame.GetDataEvent += Load;

    private void Start()
    {
        LevelInitializator.OnLevelStarted += Save;
        _load = new string[_potionStockList.Count];
        StartCoroutine(LoadData());
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

    private IEnumerator LoadData()
    {
        List<PotionForStock> tempList = new List<PotionForStock>();

        _strings = new SaveToString<PotionForStock>(_SOName);
        
        yield return new WaitForSeconds(1f);
        tempList = _strings.LoadFromStrings(_load);
        if (tempList[0].labels == null)
        {
            Save();
        }
        else
        {
            SetLoadData(tempList);
        }

    }
    private void Load()
    {
        _load = YandexGame.savesData.openPotions;
    }

    private void OnDisable()
    {
        LevelInitializator.OnLevelStarted -= Save;
            YandexGame.GetDataEvent -= Load;
        Save();
    }
}


