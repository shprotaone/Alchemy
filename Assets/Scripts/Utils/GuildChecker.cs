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
        if (value == allGuilds)
        {
            return GuildsType.All;
        }
        else if (value == saintGuild)
        {
            return GuildsType.Saint;
        }
        else if (value == knightGuild)
        {
            return GuildsType.Knight;
        }
        else if (value == wizzardGuild)
        {
            return GuildsType.Wizzard;
        }
        else if (value == banditGuild)
        {
            return GuildsType.Bandit;
        }
        else
        {
            return GuildsType.All;
        }
    }
}
