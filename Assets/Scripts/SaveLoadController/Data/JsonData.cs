using System.IO;
using UnityEngine;

public class JsonData : IData<SavedData>
{
    public void Save(SavedData data, string path = null) 
    {
        var str = JsonUtility.ToJson(data);
        File.WriteAllText(path, str);
    }

    public SavedData Load(string path = null) 
    {
        var str = File.ReadAllText(path);
        return JsonUtility.FromJson<SavedData>(str);
    }
}
