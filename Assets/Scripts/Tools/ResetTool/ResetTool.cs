using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using xPoke.CustomLog;

#if UNITY_EDITOR
public partial class ResetTool : EditorWindow
{
    [SerializeField]
    private AchievementManager achievementManager;
    [SerializeField]
    private SkinSet skinSet;

    SerializedObject serializedObject;
    SerializedProperty achievementManagerSerialized;
    SerializedProperty skinSetSerialized;

    [MenuItem("Tools/Reset Tool")]
    public static void StartEditor()
    {
        ResetTool animationCreatorEditorWindow = EditorWindow.GetWindow<ResetTool>("Reset Tool Editor", true);
        animationCreatorEditorWindow.Show();
    }

    private void OnEnable()
    {
        serializedObject = new SerializedObject(this);
        achievementManagerSerialized = serializedObject.FindProperty("achievementManager");
        skinSetSerialized = serializedObject.FindProperty("skinSet");
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space();

        serializedObject.Update();
        EditorGUILayout.PropertyField(achievementManagerSerialized, true);
        serializedObject.ApplyModifiedProperties();
        EditorGUILayout.Space();

        serializedObject.Update();
        EditorGUILayout.PropertyField(skinSetSerialized, true);
        serializedObject.ApplyModifiedProperties();
        EditorGUILayout.Space();

        if (GUILayout.Button("Reset Achievements"))
        { 
            foreach (var achievement in achievementManager.Achievements)
                achievement.Data.ResetAchievement();

            DataManager.Instance.DeleteAchievementsDataFiles();

            CustomLog.Log(CustomLog.CustomLogType.SYSTEM, "Achievements Data Reset Complete - Data files deleted and SOs resetted");
        }
        if (GUILayout.Button("Reset Skins"))
        {
            foreach (Skin skin in skinSet.Skins)
                skin.LockSkin();

            DataManager.Instance.DeleteSkinsDataFiles();

            CustomLog.Log(CustomLog.CustomLogType.SYSTEM, "Skins Data Reset Complete - Data files deleted and SOs resetted");
        }
        EditorGUILayout.EndVertical();
    }
}
#endif