using UnityEngine;
using UnityEngine.UI;

public class GuildCircleView : MonoBehaviour
{
    [SerializeField] private CircularProgressBar _saintSlider;
    [SerializeField] private CircularProgressBar _knightSlider;
    [SerializeField] private CircularProgressBar _wizzardSlider;
    [SerializeField] private CircularProgressBar _banditSlider;

    public void RefreshSlider(GuildsType type,float value)
    {
        switch (type)
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
        }
    }

}
