using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BottleView : MonoBehaviour
{
    private Vector3 standartScale = new Vector3(0.3f, 0.3f, 1);
    private Vector3 increaseScale = new Vector3(0.4f, 0.4f, 1);
    private Vector3 tradeScale = new Vector3(0.2f, 0.2f, 1);

    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private SpriteRenderer _waterInBottle;
    [SerializeField] private SpriteRenderer _bottle;
    [SerializeField] private List<SpriteRenderer> _labels;
    [SerializeField] private Wobble _wobble; 

    private LabelToSprite _labelToSprite;
    private Color _potionColor;
    private GameObject _effectInBottle;

    public List<SpriteRenderer> LabelSprites => _labels;

    public void FillColorWater(Color color)
    {
        _potionColor = color;
        _waterInBottle.enabled = true;
        _wobble.ChangeColor(_potionColor);
    }

    public void AddLabels(LabelToSprite labelToSprite, List<PotionLabelType> label)
    {
        _labelToSprite = labelToSprite;

        for (int i = 0; i < label.Count; i++)
        {
            _labels[i].sprite = _labelToSprite.GetSprite(label[i]);
        }
    }

    public void ResetView()
    {
        ReturnEffect();
        _waterInBottle.enabled = false;

        foreach (var label in _labels)
        {
            label.sprite = null;
        }
    }

    private void ReturnEffect()
    {
        if (_effectInBottle != null)
        {
            ObjectPool.SharedInstance.DestroyObject(_effectInBottle);
        }
    }

    public void SetTradeScale()
    {
        transform.DOScale(standartScale, 0.5f);
    }

    public void IncreaseSize()
    {
        transform.DOScale(increaseScale, 0.5f);
    }

    public void StandartSize()
    {
        transform.DOScale(standartScale, 0.5f);
    }

}

#region
//public void AddEffect(Potion potion)
//{
//    GameObject effect = ObjectPool.SharedInstance.GetObject(potion.EffectType);

//    if (potion.Rarity == ResourceRarity.rare)
//    {
//        _effectInBottle = effect;
//        _effectInBottle.transform.position = transform.position;
//        _effectInBottle.transform.SetParent(transform);
//        _effectInBottle.transform.localScale = new Vector3(1, 1, 0);

//        _effectInBottle.GetComponentInChildren<Effect>().ChangeParticleColor(_potionColor);
//    }
//}
#endregion
