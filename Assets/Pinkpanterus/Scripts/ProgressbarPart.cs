using UnityEngine;

[CreateAssetMenu(fileName = "ProgressbarPart", menuName = "ScriptableObjects/ProgressbarPart", order = 1)]    
public class ProgressbarPart : ScriptableObject
{
    public string InfoText;
    public Color Color;
    public PotionState PotionState;
}
