using UnityEngine;

public interface ISlot
{
    public Transform Transform { get; }
    public bool IsFree { get; }
    /// <summary>
    /// ������� �������������� ����� ����� �������� ���������� ������ ������� ���� �� ���������� ������������ ���������
    /// </summary>
    void CheckSlot();
    void SetSlot(BottleModel bottle);
    void ResetSlot();
}
