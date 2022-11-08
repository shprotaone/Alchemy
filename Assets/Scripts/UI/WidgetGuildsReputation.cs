using UnityEngine;

public class WidgetGuildsReputation : MonoBehaviour
{
    [SerializeField] private GuildReputationController _guildReputationController;
    [SerializeField] private GuildsCircleView _guildsCircleView;

    private void Start()
    {
        var data = _guildReputationController.GetAllGuildsReputation();
        _guildsCircleView.SetAllSliders(data);
    }
}
