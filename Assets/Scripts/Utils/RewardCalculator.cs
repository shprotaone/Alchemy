public class RewardCalculator
{
    private const float stockReward = 100;
    private float _reward;

    public float Reward => _reward;

    public float GetReward(int match)
    {
        if (match == 1)
        {
            _reward = stockReward;
        }
        else if (match == 2)
        {
            _reward = stockReward * 2.5f;
        }
        else if (match == 3) 
        { 
            _reward = stockReward * 6; 
        } 
        else
        {            
            _reward = 0;
        }

        return _reward;
    }
        
}

