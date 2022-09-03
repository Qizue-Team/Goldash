using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using xPoke.CustomLog;

#if UNITY_EDITOR
public partial class AchievementsResetTool : EditorWindow
{
    [SerializeField]
    private AchievementManager achievementManager;

    SerializedObject serializedObject;
    SerializedProperty achievementManagerSerialized;

    [MenuItem("Tools/Achievements Reset Tool")]
    public static void StartEditor()
    {
        AchievementsResetTool animationCreatorEditorWindow = EditorWindow.GetWindow<AchievementsResetTool>("Achievements Reset Editor", true);
        animationCreatorEditorWindow.Show();
    }

    private void OnEnable()
    {
        serializedObject = new SerializedObject(this);
        achievementManagerSerialized = serializedObject.FindProperty("achievementManager");
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space();

        serializedObject.Update();
        EditorGUILayout.PropertyField(achievementManagerSerialized, true);
        serializedObject.ApplyModifiedProperties();
        EditorGUILayout.Space();

        if (GUILayout.Button("Reset"))
        { 
            foreach (var achievement in achievementManager.Achievements)
                achievement.Data.ResetAchievement();

            DataManager.Instance.DeleteAchievementsDataFiles();

            CustomLog.Log(CustomLog.CustomLogType.SYSTEM, "Achievements Reset Complete - Data files deleted and SOs resetted");
        }

        EditorGUILayout.EndVertical();
    }
}
#endif