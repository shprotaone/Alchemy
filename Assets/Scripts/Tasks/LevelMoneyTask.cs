public class LevelMoneyTask
{
    private int _moneyTask;
    private int _minMoneyRange;

    public bool MoneyTaskComplete { get; private set; }
    public bool MoneyDefeat { get; private set; }

    public LevelMoneyTask(int moneyTask,int minRange)
    {
        _moneyTask = moneyTask;
        _minMoneyRange = minRange;

        Money.OnChangeMoney += CheckGoalMoneyTask;
        Money.OnChangeMoney += CheckFailMoneyTask;
    }

    private void CheckGoalMoneyTask(int currentMoney)
    {
        if (_moneyTask <= currentMoney)
        {
            MoneyTaskComplete = true;           
        }
    }

    public void CheckFailMoneyTask(int currentMoney)
    {
        if (_minMoneyRange >= currentMoney)
        {
            MoneyDefeat = true;       
        }
    }
}
