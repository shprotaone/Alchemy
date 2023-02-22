using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaudronEffectController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleDownBubble;
    [SerializeField] private ParticleSystem _particlelUpBubble;
    [SerializeField] private ParticleSystem _blobLeftPartical;
    [SerializeField] private ParticleSystem _blobRightPartical;

    [SerializeField] private float _stockLifeTimeUpBubble;
    [SerializeField] private float _boilLifetimeDownBubble;
    [SerializeField] private float _boilSpeed;
    [SerializeField] private float _stockSpeed;

    [SerializeField] private float _linearStockSpeedZ;
    [SerializeField] private float _linearBoilSpeedZ;

    [SerializeField] private float _orbitalStockSpeedZ;
    [SerializeField] private float _orbitalBoilSpeedZ;

    [SerializeField] private float _stockRadial;
    [SerializeField] private float _boilRadial;
    private void Start()
    {
        ChangeBehaviour(_stockSpeed, _linearStockSpeedZ, _orbitalStockSpeedZ,_stockLifeTimeUpBubble, _stockRadial);
    }

    public void Boil()
    {
        ChangeBehaviour(_boilSpeed, _linearBoilSpeedZ, _orbitalBoilSpeedZ, _boilLifetimeDownBubble, _boilRadial);
    }
    public void StopBoil()
    {
        ChangeBehaviour(_stockSpeed, _linearStockSpeedZ, _orbitalStockSpeedZ, _stockLifeTimeUpBubble, _stockRadial);
        SetColorParticle(new Color(1,1,1,0.5f));
    }

    public void IngredientBulk(Color colorInClaudron)
    {        
        var particalAcces = _blobLeftPartical.main;
        particalAcces.startColor = colorInClaudron;

        particalAcces = _blobRightPartical.main;
        particalAcces.startColor = colorInClaudron;

        SetColorParticle(colorInClaudron);

        _blobLeftPartical.Play();
        _blobRightPartical.Play();

    }
    private void SetColorParticle(Color color)
    {
        var particleDown = _particleDownBubble.colorOverLifetime;
        particleDown.color = color;

        var particleUp = _particlelUpBubble.main;
        //particleUp.startColor = color;
    }

    private void ChangeBehaviour(float speedDownBubble, float linearSpeed,float orbitalSpeed, float lifeTimeDownBubble, float radialSpeed)
    {
        var particleDown = _particleDownBubble.main;
        particleDown.startSpeed = speedDownBubble;
        particleDown.startLifetime = lifeTimeDownBubble;

        var particleUp = _particlelUpBubble.velocityOverLifetime;
        particleUp.z = linearSpeed;
        particleUp.orbitalZ = orbitalSpeed;
        particleUp.radial = radialSpeed;

    }
}
