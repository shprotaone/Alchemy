public interface ISlot
{
    public bool IsFree { get; }
    void CheckSlot();
    void SetSlot(BottleModel bottle,bool IsDraggable);
    void ResetSlot();
}
