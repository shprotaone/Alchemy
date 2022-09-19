using System.Collections.Generic;
using UnityEngine;

public class GuildSystem : MonoBehaviour
{
    private const float gultStartValue = 100;
    private const float guildCount = 4;
    private float _guildMaxValue = 100;
    
    [SerializeField] private GuildCircleView _guildCircleView;
    [SerializeField] private GuildView _guildView;

    private Dictionary<GuildsType, float> _guildDictionary;

    public float GuildMaxValue => _guildMaxValue;
    public Dictionary<GuildsType, float> GuildDictionary => _guildDictionary;

    public void InitGuildSystem()
    {
        _guildDictionary = new Dictionary<GuildsType, float>();

        for (int i = 0; i < guildCount; i++)
        {
            GuildsType guild = (GuildsType)i;
            _guildDictionary.Add(guild,gultStartValue);
            _guildCircleView.RefreshSlider(guild, gultStartValue);
            _guildView.RefreshSlider(guild, gultStartValue);
        }
    }

    public void AddRep(GuildsType type,float value)
    {
        _guildDictionary[type] += value;
        _guildCircleView.RefreshSlider(type, _guildDictionary[type]);
        _guildView.RefreshSlider(type, _guildDictionary[type]);
    }

    public void RemoveRep(GuildsType type, float value)
    {
        _guildDictionary[type] -= value;
        _guildCircleView.RefreshSlider(type, _guildDictionary[type]);
        _guildView.RefreshSlider(type, _guildDictionary[type]);
    }
}
