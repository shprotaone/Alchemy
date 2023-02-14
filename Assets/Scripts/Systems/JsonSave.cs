using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONSave<T>
{
    private string _fileName;
    private string _SOName;

    private string _dataPath;

    public JSONSave(string fileName, string SOName)
    {
        _fileName = fileName;
        _SOName = SOName;
        //SaveSystem.OnReset += ResetSkateProgress;
    }

    public List<T> LoadFromJson()
    {
        _dataPath = Path.Combine(Application.persistentDataPath, _fileName);
        Debug.Log(_dataPath);

        List<T> list = new List<T>();

        if (File.Exists(_dataPath))
        {
            string[] data = File.ReadAllLines(_dataPath);

            for (int i = 0; i < data.Length; i++)
            {
                object achievments = ScriptableObject.CreateInstance(_SOName);
                JsonUtility.FromJsonOverwrite(data[i], achievments);
                list.Add((T)achievments);
            }

            return list;
        }
        else
        {
            return list;
        }

    }

    public void SaveToJson(List<T> _achievements)
    {
        string[] data = new string[_achievements.Count];

        for (int i = 0; i < data.Length; i++)
        {
            data[i] = JsonUtility.ToJson(_achievements[i]);
        }

        File.WriteAllLines(_dataPath, data);
    }

    public void ResetAchievementsProgress()
    {
        File.Delete(_dataPath);
    }
}
