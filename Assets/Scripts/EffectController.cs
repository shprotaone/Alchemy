using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EffectController : MonoBehaviour
{
    private Image _effectImage;
    private Material _material;

    private void Start()
    {
        _effectImage = GetComponent<Image>();
    }

    public void FillEffect(Material material,Color color)
    {        
        _material = material;
        _material.color = color;
        _effectImage.material = _material;

        StartEffect();
    }

    public void StartEffect()
    {
        _material.DOFade(0, 2).OnRewind(StartEffect);
    }
}
