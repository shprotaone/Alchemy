using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public sealed class GuildReputationController : MonoBehaviour
{
    public Action<string, int> OnGuildReputationChange;
    [SerializeField] private Guild[] _guilds;
    [SerializeField] private int _guildRewardReputaion = 5;
    [SerializeField] private int _friendlyGuildRewardReputaion = 3;
    [SerializeField] private int _enemyGuildPenaltyReputaion = 2;
    private Dictionary<string, int> _playerReputation;
    public static GuildReputationController Instance { get; private set; }

    private void Awake()
    {
        //TODO use zenject
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        
        //TODO сделать загрузку из сохранения, а пока так
        _playerReputation = new Dictionary<string, int>();
        foreach (var guild in _guilds)
        {
            _playerReputation[guild._id] = 0;
        }
    }

    public string[] GetAllGuildsID()
    {
        return _guilds.Select(g=>g._id).ToArray();
    }

    public bool GetGuildReputation(string guildId, out int guildReputation)
    {
        if (_playerReputation.ContainsKey(guildId))
        {
            guildReputation = _playerReputation[guildId];
            return true;
        }
        else
        {
            guildReputation = 0;
            return false;
        }
    }

    public void ChangeReputationOnSuccefullTaskExecution(string guildId)
    {
        IncreaseReputation(guildId, _guildRewardReputaion);

        var friendlyGuilds = _guilds.Where(g => g._id == guildId).First()._friendlyGuilds;
        foreach (var friendlyGuild in friendlyGuilds)
        {
            IncreaseReputation(friendlyGuild._id, _friendlyGuildRewardReputaion);
        }
        
        var enemyGuilds = _guilds.Where(g => g._id == guildId).First()._enemyGuilds;
        foreach (var enemyGuild in enemyGuilds)
        {
            DecreaseReputation(enemyGuild._id, _enemyGuildPenaltyReputaion);
        }
        
        //TODO save _playerReputation
    }

    private void IncreaseReputation(string guildId, int changeValue)
    {
        var currentReputation = _playerReputation[guildId];
        var newReputation = Mathf.Min(currentReputation + changeValue, 100);
        _playerReputation[guildId] = newReputation;
        
        OnGuildReputationChange?.Invoke(guildId, newReputation);
        Debug.Log($"Guild: {guildId} reputation: {_playerReputation[guildId]}");
    }
    
    private void DecreaseReputation(string guildId, int changeValue)
    {
        var currentReputation = _playerReputation[guildId];
        var newReputation = Mathf.Max(currentReputation - changeValue, 0);
        _playerReputation[guildId] = newReputation;
        
        OnGuildReputationChange?.Invoke(guildId, newReputation);
        Debug.Log($"Guild: {guildId} reputation: {_playerReputation[guildId]}");
    }


    /*private void OnDisable()
    {
        //TODO save _playerReputation
    }*/
}
