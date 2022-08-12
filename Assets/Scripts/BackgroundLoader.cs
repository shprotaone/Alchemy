using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoader : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _backgroundSprite;

    public void InitBackground(Sprite sprite)
    {
        _backgroundSprite.sprite = sprite;
    }
}
