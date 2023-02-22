using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BeforeBuild))]
public class BeforeBuildEditor : Editor
{
#if UNITY_EDITOR
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BeforeBuild mytarget = (BeforeBuild)target;

        if (GUILayout.Button("Найти все файлы Ачивок"))
        {
            mytarget.FindAllAchievments();
        }

        if (GUILayout.Button(("Очистить ачивки")))
        {
            mytarget.ClearAllAchievements();
        }

        if (GUILayout.Button("Удалить прогресс"))
        {
            mytarget.ResetProgress();
        }
        EditorGUILayout.LabelField("Удаление всех сохранений");
        //GUILayout.Button("Удалить", GUILayout.Width(100), GUILayout.Height(50));
    }
#endif
}
