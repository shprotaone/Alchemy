using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class SaveToString<T>
{
    private string _SOName;

    public SaveToString(string SOName)
    {
        _SOName = SOName;
    }
    /// <summary>
    /// Используется только для яндекс игры
    /// </summary>
    /// <returns></returns>
    public List<T> LoadFromStrings(string[] loadData)
    {
        List<T> list = new List<T>();
        string[] data = loadData;

        for (int i = 0; i < data.Length; i++)
        {
            object achievments = ScriptableObject.CreateInstance(_SOName);
            JsonUtility.FromJsonOverwrite(data[i], achievments);
            list.Add((T)achievments);
        }

        return list;
    }


    public string[] SaveToList(List<T> list)
    {
        string[] data = new string[list.Count]; //количество всегда сверять с YandexGame.savesData

        for (int i = 0; i < data.Length; i++)
        {
            data[i] = JsonUtility.ToJson(list[i]);
        }

        return data;
    }
}
