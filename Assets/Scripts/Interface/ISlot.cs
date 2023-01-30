using UnityEngine;

public interface ISlot
{
    public Transform Transform { get; }
    public bool IsFree { get; }
    void CheckSlot();
    void SetSlot(BottleModel bottle);
    void ResetSlot();
}
