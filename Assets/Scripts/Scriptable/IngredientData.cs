﻿using UnityEngine;

[CreateAssetMenu]
public class IngredientData : ScriptableObject
{
    public string ingredientName;
    public int cost;
    public Color color;
    public Sprite mainSprite;
    public Sprite emptySprite;
    public Sprite dragableSprite;
    public Sprite pressedSprite;
    public ResourceType resourceType;
    public ResourceRarity resourceRarity;
    public AudioClip dragSound;
}
