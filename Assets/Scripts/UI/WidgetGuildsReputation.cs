using UnityEngine;

public sealed class WidgetGuildsReputation : MonoBehaviour
{
    [SerializeField] private GuildReputationController _guildReputationController;
    [SerializeField] private GuildsCircleView _guildsCircleView;

    private void Start()
    {
        var data = _guildReputationController.GetAllGuildsReputation();
        _guildsCircleView.SetAllSliders(data);

        _guildReputationController.OnGuildReputationChange += RefreshReputtionIndicator;
    }

    private void RefreshReputtionIndicator(GuildsType guild, int reputation)
    {
        _guildsCircleView.RefreshSlider(guild, reputation);
    }

    private void OnDisable()
    {
        _guildReputationController.OnGuildReputationChange -= RefreshReputtionIndicator;
    }
}
