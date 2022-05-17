using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Cauldrons : ScriptableObject
{
    public string cauldronName;
    public float chance;
    public float cooldown;
    public float speedMul;
    public Sprite image;
}
