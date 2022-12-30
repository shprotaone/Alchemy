using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ClaudronLabelView : MonoBehaviour
{
    [SerializeField] private List<Sprite> _sprites;
    [SerializeField] private LabelToSprite _labelToSprite;

    private void Start()
    {
        _sprites = new List<Sprite>();
    }

    public void SetLabel(List<PotionLabelType> labels)
    {
        for (int i = 0; i < labels.Count; i++)
        {
            _sprites[i] = _labelToSprite.GetSprite(labels[i]);
        }
    }

    private void Reset()
    {
        for (int i = 0; i < _sprites.Count; i++)
        {
            _sprites[i] = null;
        }
    }
}
