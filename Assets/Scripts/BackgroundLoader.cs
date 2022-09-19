using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoader : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _backgroundSprite;

    private void Awake()
    {
        LevelInitializator.OnBackGroundInit += InitBackground;
    }
    /// <summary>
    /// Подгрузка заднего фона для конкретного уровня
    /// </summary>
    /// <param name="sprite"></param>
    private void InitBackground(Sprite sprite)
    {
        _backgroundSprite.sprite = sprite;
    }

    private void OnDestroy()
    {
        LevelInitializator.OnBackGroundInit -= InitBackground;
    }
}
