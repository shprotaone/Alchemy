using UnityEngine;

public sealed class PlayerPrefsData : IData<SavedData>
{
    public void Save(SavedData data, string path = null) 
    {
        var savingData = JsonUtility.ToJson(data);
        //var key = SaveLoadRepository.PlayerPrefsKey;
        PlayerPrefs.SetString(path, savingData);
        PlayerPrefs.Save();
    }

    public SavedData Load(string path = null)
    {
        //var key = SaveLoadRepository.PlayerPrefsKey;
        var loadedData = PlayerPrefs.GetString(path);
        return JsonUtility.FromJson<SavedData>(loadedData);
    }
}
