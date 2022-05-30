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

    public GuildsType GuildCheck(PotionData potionData)
    {
        if (potionData.guild == allGuilds)
        {
            return GuildsType.All;
        }
        else if (potionData.guild == saintGuild)
        {
            return GuildsType.Saint;
        }
        else if (potionData.guild == knightGuild)
        {
            return GuildsType.Knight;
        }
        else if (potionData.guild == wizzardGuild)
        {
            return GuildsType.Wizzard;
        }
        else if (potionData.guild == banditGuild)
        {
            return GuildsType.Bandit;
        }
        else
        {
            return GuildsType.All;
        }
    }
}
