using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Effect : MonoBehaviour,IPooledObject
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Light2D _light2d;
    [SerializeField] private ObjectType _type;

    public ObjectType Type => _type;

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
