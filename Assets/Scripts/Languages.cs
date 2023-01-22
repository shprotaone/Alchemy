using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Languages : ScriptableObject
{
    [TextArea(5,100)]
    [SerializeField] public List<string> _guideTextRU;
}
