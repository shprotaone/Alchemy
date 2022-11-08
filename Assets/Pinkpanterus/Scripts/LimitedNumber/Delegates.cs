
namespace LimitedNumbers
{
    public delegate void ValueSetupedDelegate(int value);
    
    public delegate void ValueIncrementedDelegate(int previousValue, int newValue);

    public delegate void ValueDecrementedDelegate(int previousValue, int newValue);
    
    public delegate void ValueChangedDelegate(int previousValue, int newValue);
}

