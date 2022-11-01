using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReputationReducer : MonoBehaviour
{
    [SerializeField] private GuildSystem _guildSystem;
    [SerializeField] private GameTimer _timer;

    private int _valueRep;
    private int _timerVal;

    public void InitReducer(int valueRep, int timer)
    {
        _valueRep = valueRep;
        _timerVal = timer;
        GameTimer.OnSecondChange += Reduce;
    }

    public void Reduce(float time)
    {
        if (time % _timerVal == 0) 
        {
            _guildSystem.RemoveRep(GuildsType.Saint, _valueRep);
            _guildSystem.RemoveRep(GuildsType.Knight, _valueRep);
            _guildSystem.RemoveRep(GuildsType.Wizzard, _valueRep);
            _guildSystem.RemoveRep(GuildsType.Bandit, _valueRep);
        }
    }

    private void OnDisable()
    {
        GameTimer.OnSecondChange -= Reduce;
    }
}
