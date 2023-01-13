using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GuideController : MonoBehaviour
{
    [SerializeField] private CircularProgressBar _progressBar;
    [SerializeField] private Button _playButton;
    [SerializeField] private ScrollRect _contentTransform;
    [SerializeField] private List<RectTransform> _contents;

    private void Start()
    {
        _playButton.onClick.AddListener(PlayManual);
    }

    private void OnEnable()
    {
        _contentTransform.content.anchoredPosition = SetContentPos(0);
    }

    private Vector2 SetContentPos(float leftPos)
    {
        return new Vector2(leftPos, _contentTransform.content.anchoredPosition.y);
    }
    private void PlayManual()
    {
        StartCoroutine(ManualAnim());
    }

    private IEnumerator ManualAnim()
    {
        int currentContentIndex = 1;

        while(currentContentIndex <= _contents.Count-1)
        {
            yield return new WaitForSeconds(3f);
            Vector2 nextPos = SetContentPos(-_contents[currentContentIndex].anchoredPosition.x);
            _contentTransform.content.DOAnchorPos(nextPos, 1);                         
            currentContentIndex++;
        }
    }
}
