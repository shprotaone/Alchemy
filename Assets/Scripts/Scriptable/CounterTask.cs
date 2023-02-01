using System;

public class RandomPart :  IComparable<RandomPart>
{
    private int _count;
    private int _weight;

    public int Count => _count;
    public int Weight => _weight;
    public RandomPart(int count,int weight)
    {
        _count = count;
        _weight = weight;
    }

    public int CompareTo(RandomPart task)
    {
        if(task == null)
            return 1;
        else
            return this._weight.CompareTo(task._weight);
    }

    public override int GetHashCode()
    {
        return _weight;
    }

    public bool Equals(RandomPart task)
    {
        if (task == null) return false;
        return this._weight.Equals(task._weight);
    }
}
