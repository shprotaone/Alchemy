using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using DG.Tweening;

public class LightController : MonoBehaviour
{
    [SerializeField] private Light2D _light;
    [SerializeField] private float _intencity = 3f;
    [SerializeField] private float _duration;
    [SerializeField] private Ease _easy;
    // Start is called before the first frame update
    void Start()
    {      
        DOTween.To(() => _intencity, x => _intencity = x, 0.2f, _duration).SetLoops(-1,LoopType.Yoyo)
            .SetEase(_easy)
            .OnUpdate(() => _light.intensity = _intencity);
        
    }
}
