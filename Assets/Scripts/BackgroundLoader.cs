using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoader : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _backgroundSprite;

    public void SetBackGround(Sprite sprite)
    {
        if(sprite != null)
            _backgroundSprite.sprite = sprite;
    }
}
