using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Claudron : ScriptableObject
{
    public ClaudronType cauldronType;
    public float chance;
    public float cooldown;
    public float speedMul;
    public Sprite image;
}
