using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public sealed class SaveLoadController : Singleton<SaveLoadController>
{
    [SerializeField] private SaveDataType _currentSaveDataType;

    public enum SaveDataType 
    {
        playerPrefsData,
        jsonData,
        xmlData
    }

    private IData<SavedData> _data;

    private const string _playerPrefsKey = "SaveData";
    private const string _folderName = "SaveData";
    private const string _fileName = "data.dat";
    private  string _path;

    private void Awake()
    {
        _path = Path.Combine(Application.dataPath, _folderName);
        //Debug.Log("Path = " + _path);
        
        GuildReputationController.Instance.OnGuildsReputationChange += Save;
    }


    /*public SaveLoadRepository(SaveDataType saveDataType)
    {
        _currentSaveDataType = saveDataType;
        
        switch (saveDataType) 
        {
            case SaveDataType.playerPrefsData:
                _data = new PlayerPrefsData();
                break;
            case SaveDataType.jsonData:
                _data = new JsonData();
                break;
            case SaveDataType.xmlData:
                _data = new XMLData();
                break;
        }
    }*/
    
    private void Save(Dictionary<GuildsType, int> guildsReputation)
    {
        var saveObject = new SavedData(guildsReputation);
        ExecuteSava(saveObject);
    }

    public void Save() 
    {
        var reputationDictionary = GuildReputationController.Instance.GetAllGuildsReputation();
        var saveObject = new SavedData(reputationDictionary);

        ExecuteSava(saveObject);
    }

    private void ExecuteSava(SavedData saveObject)
    {
        switch (_currentSaveDataType)
        {
            case SaveDataType.playerPrefsData:
                _data = new PlayerPrefsData();
                break;
            case SaveDataType.jsonData:
                _data = new JsonData();
                break;
            case SaveDataType.xmlData:
                _data = new XMLData();
                break;
        }

        if (_currentSaveDataType == SaveDataType.playerPrefsData)
        {
            _data.Save(saveObject, _playerPrefsKey);
        }
        else
        {
            if (!Directory.Exists(Path.Combine(_path)))
            {
                Directory.CreateDirectory(_path);
            }

            _data.Save(saveObject, Path.Combine(_path, _fileName));
        }
    }

    public SavedData Load() 
    {
        if (_currentSaveDataType == SaveDataType.playerPrefsData)
        {
            if (!PlayerPrefs.HasKey(_playerPrefsKey)) return new SavedData();
            
            SavedData savedData = _data.Load(_playerPrefsKey);
            return savedData;
        }
        else
        {
            SavedData savedData = _data.Load(Path.Combine(_path, _fileName));
            return savedData;
        }
    }
}
