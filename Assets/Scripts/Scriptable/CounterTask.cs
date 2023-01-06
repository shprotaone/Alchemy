using UnityEngine;

public class CounterTask
{
    private int _count;
    private int _weight;

    public int Count => _count;
    public int Weight => _weight;
    public CounterTask(int count,int weight)
    {
        _count = count;
        _weight = weight;
    }
}
