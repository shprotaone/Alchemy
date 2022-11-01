using System;
using System.Collections.Generic;

[Serializable]
public struct SavedData 
{
    public Dictionary<GuildsType, int> GuildsReputation;

    public SavedData(Dictionary<GuildsType, int> reputationDict)
    {
        GuildsReputation = reputationDict;
    }
}

