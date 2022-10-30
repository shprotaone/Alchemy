#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

public sealed class GuildKeyAttributeDrawer : OdinAttributeDrawer<GuildKeyAttribute, string>
{
    protected override void DrawPropertyLayout(GUIContent label)
    {
        var guildsNames = GuildReputationController.Instance.GetAllGuildsID();
        if (guildsNames.Length <= 0)
        {
            return;
        }

        GUIHelper.PushLabelWidth(GUIHelper.BetterLabelWidth);
            
        var name = this.ValueEntry.SmartValue;
        if (string.IsNullOrEmpty(name))
        {
            name = guildsNames[0];
        }

        var currentIndex = 0;
        if (Array.Exists(guildsNames, it => it == name))
        {
            currentIndex = Array.IndexOf(guildsNames, name);
        }
            
        currentIndex = EditorGUILayout.Popup(label, currentIndex, guildsNames);
        ValueEntry.SmartValue = guildsNames[currentIndex];

        GUIHelper.PopLabelWidth();
    }
}
#endif