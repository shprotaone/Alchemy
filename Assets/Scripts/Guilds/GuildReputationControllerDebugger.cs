using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GuildReputationControllerDebugger : MonoBehaviour
{
    private GuildReputationController _guildReputationController;
    void Start()
    {
        _guildReputationController = GuildReputationController.Instance;
    }

    [Button("Show Guilds Names"), GUIColor(0,1,0)]
    public void ShowGuildsNames()
    {
        var names = _guildReputationController.GetAllGuildsID();
        foreach (var name in names)
        {
            Debug.Log(name);
        }
    }
    
    [Button("Show Guilds Names"), GUIColor(1,0,0)]
    public void CheckReputationChange(string guild)
    {
        _guildReputationController.ChangeReputationOnSuccefullTaskExecution(guild);
    }
}

