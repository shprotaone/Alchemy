using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoader : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _backgroundSprite;

    /// <summary>
    /// ��������� ������� ���� ��� ����������� ������
    /// </summary>
    /// <param name="sprite"></param>
    public void InitBackground(Sprite sprite)
    {
        _backgroundSprite.sprite = sprite;
    }
}
