using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Potion : ScriptableObject
{
    public Sprite imageBottle;
    public Sprite imageWater;
    public PotionColor color;
    public PotionEffect effect;
    public Color effectColor;
    public Sprite[] colored;
    public Sprite rare;
    public int id;
}