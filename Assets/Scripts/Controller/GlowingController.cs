using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GlowingController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _fillBottleSprite;
    private Light2D _glowLight;

    [Range(0f,10f)]
    public float _timeLighting;
    private float _maxIntensity = 4;

    private void Start()
    {
        _glowLight = GetComponentInChildren<Light2D>();

        StartCoroutine(Blinking());
    }

    private IEnumerator Blinking()
    {
        float waitTime = _timeLighting / 2;
        _glowLight.color = _fillBottleSprite.color;

        while (true)
        {
            while (_glowLight.intensity < _maxIntensity)
            {
                _glowLight.intensity += Time.deltaTime / waitTime;
                yield return null;
            }

            while (_glowLight.intensity > 0)
            {
                _glowLight.intensity -= Time.deltaTime / waitTime;
                yield return null;
            }
        }
    }
}
