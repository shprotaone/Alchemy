using UnityEngine;

public class Wobble : MonoBehaviour
{
    private const float alfaValue = 0.6f;
    [SerializeField] private SpriteRenderer _renderer;

    public void ChangeColor(Color color)
    {
        _renderer.color = new Color(color.r, color.g, color.b, alfaValue);

    }
}
