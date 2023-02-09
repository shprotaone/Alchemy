using UnityEngine;

[CreateAssetMenu]
public class IngredientData : ScriptableObject
{
    public string ingredientName;
    public int cost;
    public Color color;
    public Sprite mainSprite;
    public Sprite dragableSprite;
    public ResourceType resourceType;
    public ResourceRarity resourceRarity;
    public AudioClip dragSound;
}
