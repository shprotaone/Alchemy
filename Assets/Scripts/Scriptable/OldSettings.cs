using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class OldSettings : ScriptableObject
{ 
    [Header("Время варки зелья из 2 ингредиентов, сек.")]
    public int timeBrew2;

    [Header("Время варки зелья из 3 ингредиентов, сек.")]
    public int timeBrew3;

    [Header("Время варки зелья из 4 ингредиентов, сек.")]
    public int timeBrew4;

    [Header("Время варки редкого зелья, сек.")]
    public int timeBrewRare;

    [Header("Время действия дров, сек.")]
    public int timeWood;

}