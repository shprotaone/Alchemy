using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DayNotifyTextVariant : ScriptableObject
{
    [TextArea(3, 50)]
    public List<string> waterTexts;
    [TextArea(3, 50)]
    public List<string> fireTexts;
    [TextArea(3, 50)]
    public List<string> stoneTexsts;
}
