using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BottleView : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;

    [SerializeField] private SpriteRenderer _waterInBottle;
    [SerializeField] private SpriteRenderer _bottle;
    [SerializeField] private List<SpriteRenderer> _labels;
    [SerializeField] private Wobble _wobble; 
    private LabelToSprite _labelToSprite;
    private Color _potionColor;
    private GameObject _effectInBottle;

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

    public void AddEffect(Potion potion)
    {
        GameObject effect = ObjectPool.SharedInstance.GetObject(potion.EffectType);

        if (potion.Rarity == ResourceRarity.rare)
        {
            _effectInBottle = effect;
            _effectInBottle.transform.position = transform.position;
            _effectInBottle.transform.SetParent(transform);
            _effectInBottle.transform.localScale = new Vector3(1, 1, 0);

            _effectInBottle.GetComponentInChildren<Effect>().ChangeParticleColor(_potionColor);
        }
    }

    public void ResetView()
    {
        ReturnEffect();

        _waterInBottle.enabled = false;
    }

    private void ReturnEffect()
    {
        if (_effectInBottle != null)
        {
            ObjectPool.SharedInstance.DestroyObject(_effectInBottle);
        }
    }
}
