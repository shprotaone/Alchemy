using UnityEngine;

public class ReputationReducer : IDragTimer
{
    private GuildSystem _guildSystem;
    private LocalTimer _timer;

    private int _valueRep;
    private int _intervaltime;

    public void InitReducer(GuildSystem guildSystem, int valueRep, int timer)
    {
        _valueRep = valueRep;
        _intervaltime = timer;
        _guildSystem = guildSystem;
        
        InitTimer(_intervaltime);

        if(_valueRep != 0 || _intervaltime != 0)
        {
            //GameTimer.OnSecondChange += Reduce;
        }
        else
        {
            Debug.LogWarning("не указано значение репутации или не указан интервал");
        }      
    }

    public void InitTimer(int delayDrag)
    {
        _timer = new LocalTimer(_intervaltime, false);
    }

    public void Reduce(int time)
    {
        if (time % _intervaltime == 0) 
        {
            _guildSystem.RemoveRep(GuildsType.Saint, _valueRep);
            _guildSystem.RemoveRep(GuildsType.Knight, _valueRep);
            _guildSystem.RemoveRep(GuildsType.Wizzard, _valueRep);
            _guildSystem.RemoveRep(GuildsType.Bandit, _valueRep);
        }
    }

    public void StartTimer()
    {
        _timer.StartTimer();
    }

    private void OnDisable()
    {
        //GameTimer.OnSecondChange -= Reduce;
    }
}
