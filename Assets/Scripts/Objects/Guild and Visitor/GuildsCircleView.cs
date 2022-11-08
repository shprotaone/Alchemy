using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GuildsCircleView : MonoBehaviour
{
    [SerializeField] private GuildIndicatorSet[] _guildIndicatorSets;
    
    /*[Space]
    [SerializeField] private CircularProgressBar _saintSlider;
    [SerializeField] private CircularProgressBar _knightSlider;
    [SerializeField] private CircularProgressBar _wizzardSlider;
    [SerializeField] private CircularProgressBar _banditSlider;*/

    public void RefreshSlider(GuildsType type,float value)
    {
        var slider = _guildIndicatorSets.Where(g => g._guild == type).First()._progressBar;
        slider.SetValue(value/100);

        /*switch (type)
        {
            case GuildsType.Saint:
                _saintSlider.SetValue(value/100);
                break;
            case GuildsType.Knight:
                _knightSlider.SetValue(value / 100);
                break;
            case GuildsType.Wizzard:
                _wizzardSlider.SetValue(value / 100);
                break;
            case GuildsType.Bandit:
                _banditSlider.SetValue(value / 100);
                break;
        }*/
    }

    public void RefreshSlider(GuildsType type, int value)
    {
        var slider = _guildIndicatorSets.Where(g => g._guild == type).First()._progressBar;
        slider.SetValue(value / 100);
    }

    public void SetAllSliders(Dictionary<GuildsType, int> guildsReputation)
    {
        foreach (var guild in guildsReputation.Keys)
        {
            if (Array.Exists(_guildIndicatorSets, x => x._guild == guild))
            {
                var slider = _guildIndicatorSets.Where(g=>g._progressBar).First()._progressBar;
                var value = guildsReputation[guild] / 100;
                slider.SetValue(value);
            }
        }
    }


}
