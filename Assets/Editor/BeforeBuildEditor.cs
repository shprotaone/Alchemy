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

        if (GUILayout.Button("����� ��� ����� ������"))
        {
            mytarget.FindAllAchievments();
        }

        if (GUILayout.Button(("�������� ������")))
        {
            mytarget.ClearAllAchievements();
        }

        if (GUILayout.Button("������� ��������"))
        {
            mytarget.ResetProgress();
        }
        EditorGUILayout.LabelField("�������� ���� ����������");
        //GUILayout.Button("�������", GUILayout.Width(100), GUILayout.Height(50));
    }
#endif
}
