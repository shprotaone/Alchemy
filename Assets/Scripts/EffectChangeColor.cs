using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class EffectChangeColor : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Light2D _light2d;

    public void ChangeParticleColor(Color color)
    {
        if(_particleSystem != null)
        {
            var main = _particleSystem.main;
            main.startColor = color;
        }
        else if(_light2d != null)
        {
            _light2d.color = color;
        }
        else
        {
            print("no effect");
        }
        
    }
}
