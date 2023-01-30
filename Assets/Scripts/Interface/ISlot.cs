using UnityEngine;

public interface ISlot
{
    public Transform Transform { get; }
    public bool IsFree { get; }
    /// <summary>
    /// Система взаимодейтсвий через метод проверки пердыдущих слотов помогла уйти от паразитных срабатываний триггеров
    /// </summary>
    void CheckSlot();
    void SetSlot(BottleModel bottle);
    void ResetSlot();
}
