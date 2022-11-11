using UnityEditor;
using UnityEngine;

//[CustomEditor(typeof(LevelPreset))]
public class DropDownSO : Editor
{
    public LevelNumber levelNumber;
    public override void OnInspectorGUI()
    {            
        var LevelPreset = target as LevelPreset;

       // LevelPreset.levelNumber = (LevelNumber)EditorGUILayout.EnumPopup("Level", levelNumber);


        LevelPreset.DropDownMenu = GUILayout.Toggle(LevelPreset.DropDownMenu, "Does it have a launcher");
        if (LevelPreset.DropDownMenu)
        {
            LevelPreset.startMoney = EditorGUILayout.IntField("StartMoney", LevelPreset.startMoney);
            LevelPreset.minRangeMoney = EditorGUILayout.IntField("Projectile Gravity", LevelPreset.minRangeMoney);
            LevelPreset.MoneyGoal = EditorGUILayout.IntField("Projectiles", LevelPreset.MoneyGoal);
        }
    }    
}
