using UnityEngine;
using UnityEngine.UI;

public class GuildView : MonoBehaviour
{
    [SerializeField] private Slider _saintSlider;
    [SerializeField] private Slider _knightSlider;
    [SerializeField] private Slider _wizzardSlider;
    [SerializeField] private Slider _banditSlider;
    [SerializeField] private GuildSystem _guildSystem;

    private void Start()
    {
        InitSlider();
    }
    private void InitSlider()
    {
        _saintSlider.maxValue = _guildSystem.GuildMaxValue;
        _banditSlider.maxValue = _guildSystem.GuildMaxValue;
        _knightSlider.maxValue = _guildSystem.GuildMaxValue;
        _wizzardSlider.maxValue = _guildSystem.GuildMaxValue;
    }

    public void RefreshSlider(GuildsType type, float value)
    {
        switch (type)
        {
            case GuildsType.Saint:
                _saintSlider.value = value;
                break;
            case GuildsType.Knight:
                _knightSlider.value = value;
                break;
            case GuildsType.Wizzard:
                _wizzardSlider.value = value;
                break;
            case GuildsType.Bandit:
                _banditSlider.value = value;
                break;
        }
    }
}
