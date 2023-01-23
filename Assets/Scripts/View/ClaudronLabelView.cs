using System.Collections.Generic;
using UnityEngine;

public class ClaudronLabelView : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> _sprites;    
    [SerializeField] private LabelToSprite _labelToSprite;

    public void SetLabel(List<PotionLabelType> labels)
    {
        for (int i = 0; i < labels.Count; i++)
        {
            _sprites[i].sprite = _labelToSprite.GetSprite(labels[i],true);
        }
    }

    public void Reset()
    {
        for (int i = 0; i < _sprites.Count; i++)
        {
            _sprites[i].sprite = null;
        }
    }
}
