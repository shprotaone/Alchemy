using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalController : MonoBehaviour
{
    [SerializeField] private LevelSelector _gameProgress;
    [SerializeField] private List<StoryContent> _contents;

    //private void Start()
    //{
    //    for (int i = 0; i < _gameProgress.LevelReached; i++)
    //    {
    //        _contents[i].UnlockStory();
    //    }
    //}
}
