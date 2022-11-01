using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//[ExecuteInEditMode]
public sealed class GuildReputationController : Singleton<GuildReputationController>
{
    public Action<GuildsType, int> OnGuildReputationChange;
    public Action<Dictionary<GuildsType, int>> OnGuildsReputationChange;
    [SerializeField] private Guild[] _guilds;
    [SerializeField] private int _guildRewardReputaion = 5;
    [SerializeField] private int _friendlyGuildRewardReputaion = 3;
    [SerializeField] private int _enemyGuildPenaltyReputaion = 2;
    private Dictionary<GuildsType, int> _playerReputation;
    
    private void Awake()
    {
        var data = SaveLoadController.Instance.Load();

        if (data.GuildsReputation == null)
        {
            _playerReputation = new Dictionary<GuildsType, int>();
            foreach (var guild in _guilds)
            {
                _playerReputation[guild.CurrentGuild] = 80;
            }
        }
        else
        {
            _playerReputation = data.GuildsReputation;
        }
    }

    public GuildsType[] GetAllGuildsID()
    {
        return _guilds.Select(g=>g.CurrentGuild).ToArray();
    }

    public Dictionary<GuildsType, int> GetAllGuildsReputation()
    {
        return _playerReputation;
    }

    public bool GetGuildReputation(GuildsType guild, out int guildReputation)
    {
        if (_playerReputation.ContainsKey(guild))
        {
            guildReputation = _playerReputation[guild];
            return true;
        }
        else
        {
            guildReputation = 0;
            return false;
        }
    }

    public void ChangeReputationOnSuccefullTaskExecution(GuildsType guild)
    {
        IncreaseReputation(guild, _guildRewardReputaion);

        var friendlyGuilds = _guilds.Where(g => g.CurrentGuild == guild).First().FriendlyGuilds;
        foreach (var friendlyGuild in friendlyGuilds)
        {
            IncreaseReputation(friendlyGuild.CurrentGuild, _friendlyGuildRewardReputaion);
        }
        
        var enemyGuilds = _guilds.Where(g => g.CurrentGuild == guild).First().EnemyGuilds;
        foreach (var enemyGuild in enemyGuilds)
        {
            DecreaseReputation(enemyGuild.CurrentGuild, _enemyGuildPenaltyReputaion);
        }
        
        OnGuildsReputationChange?.Invoke(_playerReputation);
    }

    private void IncreaseReputation(GuildsType guild, int changeValue)
    {
        var currentReputation = _playerReputation[guild];
        var newReputation = Mathf.Min(currentReputation + changeValue, 100);
        _playerReputation[guild] = newReputation;
        
        OnGuildReputationChange?.Invoke(guild, newReputation);
        //Debug.Log($"Guild: {guild} reputation: {_playerReputation[guild]}");
    }
    
    private void DecreaseReputation(GuildsType guild, int changeValue)
    {
        var currentReputation = _playerReputation[guild];
        var newReputation = Mathf.Max(currentReputation - changeValue, 0);
        _playerReputation[guild] = newReputation;
        
        OnGuildReputationChange?.Invoke(guild, newReputation);
        //Debug.Log($"Guild: {guild} reputation: {_playerReputation[guild]}");
    }
}
