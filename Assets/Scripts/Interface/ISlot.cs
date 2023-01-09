public interface ISlot
{
    public bool IsFree { get; }
    void CheckSlot();
    void SetSlot(Bottle bottle);
    void ResetSlot();
}
