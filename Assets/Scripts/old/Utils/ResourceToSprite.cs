using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceToSprite : MonoBehaviour
{
    [SerializeField] private Sprite _redSprite;
    [SerializeField] private Sprite _yellowSprite;
    [SerializeField] private Sprite _blueSprite;
    [SerializeField] private Sprite _whiteSprite;

    public Sprite Convert(ResourceType type)
    {
        if (type == ResourceType.White) return _whiteSprite;
        else if (type == ResourceType.Red) return _redSprite;
        else if (type == ResourceType.Yellow) return _yellowSprite;
        else return _blueSprite;
    }
      
}
