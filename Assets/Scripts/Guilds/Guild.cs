using UnityEngine;

[CreateAssetMenu(fileName = "Faction", menuName = "ScriptableObjects/Faction", order = 1)]
public sealed class Guild : ScriptableObject
{
    public string _id;
    public Guild[] _enemyGuilds;
    public Guild[] _friendlyGuilds;
}
