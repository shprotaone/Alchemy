using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class AchievementsProgressSaver : MonoBehaviour
{
    //private readonly string _fileName = "AchievmentsProgress.json";   //для сохранения на Android
    private readonly string _soName = "AchievementData";

    [SerializeField] private List<AchievementData> _achievementData;
    private ISubject[] _subjectsList;
    private GameProgressSaver _saver;
    //private JSONSave<AchievementData> _json;
    private SaveToString<AchievementData> _strings;

    private void OnEnable() => YandexGame.GetDataEvent += LoadData;

    private void Start()
    {
        InitSubjects();
        if (YandexGame.SDKEnabled == true)
        {
            LoadData();
        }

        LevelInitializator.OnLevelStarted += SaveProgress ;

        StartCoroutine(AutoSaveRoutine());
    }

    public void LoadData()
    {
        _strings = new SaveToString<AchievementData>(_soName);

        List<AchievementData> list = new List<AchievementData>();
        list = _strings.LoadFromStrings(YandexGame.savesData.achievmentsStrings);

        if (list[0].Goal == 0)
        {
            SaveProgress();
        }
        else
        {
            SetLoadData(list); ;
        }
    }

    public void GameProgressSaverHandler(GameProgressSaver saver)
    {
        _saver = saver;
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
        //_json.SaveToJson(_achievementData);
        YandexGame.savesData.achievmentsStrings = _strings.SaveToList(_achievementData);
        YandexGame.SaveProgress();
    }

    private void OnDisable()
    {
        SaveProgress();
        StopCoroutine(AutoSaveRoutine());
        YandexGame.GetDataEvent -= LoadData;
        LevelInitializator.OnLevelStarted -= SaveProgress;
    }
}

