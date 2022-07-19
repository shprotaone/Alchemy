using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildChecker
{
    public const string allGuilds = "all";
    public const string saintGuild = "Saint";
    public const string knightGuild = "Knight";
    public const string wizzardGuild = "Wizzard";
    public const string banditGuild = "Bandit";

    public GuildsType GuildCheck(string value)
    {
        return value switch
        {
            allGuilds => GuildsType.All,
            saintGuild => GuildsType.Saint,
            knightGuild => GuildsType.Knight,
            wizzardGuild => GuildsType.Wizzard,
            banditGuild => GuildsType.Bandit,
            _ => GuildsType.All
        };
    }
}
