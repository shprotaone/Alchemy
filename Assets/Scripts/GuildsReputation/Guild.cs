using UnityEngine;

[CreateAssetMenu(fileName = "Faction", menuName = "ScriptableObjects/Faction", order = 1)]
public sealed class Guild : ScriptableObject
{
    public string Id;
    public GuildsType CurrentGuild;
    public Guild[] EnemyGuilds;
    public Guild[] FriendlyGuilds;
    public IngredientData PortionForGoodReputation;

}
