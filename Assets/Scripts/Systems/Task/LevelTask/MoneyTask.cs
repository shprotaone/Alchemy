public class MoneyTask
{
    private int _moneyTask;

    public int TaskMoney => _moneyTask;

    public MoneyTask(int currentTask)
    {
        _moneyTask = currentTask;
    }

    public void IncreaseTask(int currentMoney,int taskStep)
    {
        _moneyTask = currentMoney + taskStep;
    }
}
