using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryContent : MonoBehaviour
{
    [SerializeField] private Transform _lockSprite;

    public void UnlockStory()
    {
        _lockSprite.gameObject.SetActive(false);
    }
}
