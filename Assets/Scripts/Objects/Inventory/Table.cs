using UnityEngine;
using System;

public class Table : MonoBehaviour
{
    private const float sizeOffset = 0.8f;

    [SerializeField] private bool _fullPotionTable;
    private Bottle _bottle;

    private float _offset;
    private float[] _startPositions;

    public bool FullPotionTable => _fullPotionTable;

    public Vector2 SetStartPosition(float index)
    {
        float offset = index * sizeOffset;
        Vector3 result = transform.position + new Vector3(offset, 0, 0);

        return result;
    }

    public void SortBottlePosition()
    {
        if (_fullPotionTable)
        {
            for (int i = 0; i < transform.childCount; i++)
            {               
                transform.GetChild(i).position = transform.position + new Vector3(sizeOffset +_offset, 0, 0);
                _offset += sizeOffset;
            }
        }
        _offset = 0;
    }
}
