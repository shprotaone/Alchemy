using System.Collections.Generic;
using UnityEngine;

public class AchievementsProgressSaver : MonoBehaviour
{
    private string _fileName = "AchievmentsProgress.json";
    private string _SOName = "AchievementData";

    [SerializeField] private List<AchievementData> _achievementData; 
    [SerializeField] private PotionStock _stock;
    [SerializeField] private CookSubject _cookSubject;

    private JSONSave<AchievementData> _json;

    private void Start()
    {
        _cookSubject.Init(_achievementData);

        _json = new JSONSave<AchievementData>(_fileName, _SOName);

        List<AchievementData> list = new List<AchievementData>();
        list = _json.LoadFromJson();

        if (list.Count == 0)
        {
            SaveProgress();
        }        
        else
        {
            SetLoadData(list);;
        }

        LevelInitializator.OnLevelStarted += SaveProgress;
    }

    private void SetLoadData(List<AchievementData> achievements)
    {
        for (int i = 0; i < _achievementData.Count; i++)
        {
            _achievementData[i].complete = achievements[i].complete;
            _achievementData[i].goalProgress = achievements[i].goalProgress;
            _achievementData[i].goal = achievements[i].goal;
        }
    }

    public void SaveProgress()
    {
        _json.SaveToJson(_achievementData);
    }

    private void OnDisable()
    {
        SaveProgress();
        LevelInitializator.OnLevelStarted -= SaveProgress;
    }
}

