using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsProgressSaver : MonoBehaviour
{
    private readonly string _fileName = "AchievmentsProgress.json";
    private readonly string _soName = "AchievementData";

    [SerializeField] private List<AchievementData> _achievementData;
    private ISubject[] _subjectsList;

    private JSONSave<AchievementData> _json;

    private void Start()
    {
        InitSubjects();

        _json = new JSONSave<AchievementData>(_fileName, _soName);

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

        StartCoroutine(AutoSaveRoutine());
    }

    private void InitSubjects()
    {
        _subjectsList = GetComponents<ISubject>();

        for (int i = 0; i < _subjectsList.Length; i++)
        {
            _subjectsList[i].Init(_achievementData);
        }
    }

    private void SetLoadData(List<AchievementData> achievements)
    {
        for (int i = 0; i < _achievementData.Count; i++)
        {
            _achievementData[i].Complete = achievements[i].Complete;
            _achievementData[i].GoalProgress = achievements[i].GoalProgress;
            _achievementData[i].Goal = achievements[i].Goal;
        }
    }

    private IEnumerator AutoSaveRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(30);
            SaveProgress();
        }
    }

    public void SaveProgress()
    {
        _json.SaveToJson(_achievementData);
    }

    private void OnDisable()
    {
        SaveProgress();
        StopCoroutine(AutoSaveRoutine());
        LevelInitializator.OnLevelStarted -= SaveProgress;
    }
}

