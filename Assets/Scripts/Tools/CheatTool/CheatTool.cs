using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using xPoke.CustomLog;

#if UNITY_EDITOR
public partial class CheatTool : EditorWindow
{
    [SerializeField]
    private AchievementManager achievementManager;
    [SerializeField]
    private SkinSet skinSet;

    private SerializedObject _serializedObject;
    private int _gearCheatAmount = 0;

    [MenuItem("Tools/Cheat Tool")]
    public static void StartEditor()
    {
        CheatTool animationCreatorEditorWindow = EditorWindow.GetWindow<CheatTool>("Cheat Tool Editor", true);
        animationCreatorEditorWindow.Show();
    }

    private void OnEnable()
    {
        _serializedObject = new SerializedObject(this);
        
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        
        EditorGUILayout.LabelField("TotalGear");
        _gearCheatAmount = EditorGUILayout.IntField(_gearCheatAmount);
       
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Add Gears"))
        {
            DataManager.Instance.SaveTotalGearCount(_gearCheatAmount);
            CustomLog.Log(CustomLog.CustomLogType.SYSTEM, "New Total Gear Amount: "+_gearCheatAmount);
        }

        if (GUILayout.Button("Reset Tutorial"))
        {
            DataManager.Instance.ResetTutorialFlag();
            CustomLog.Log(CustomLog.CustomLogType.SYSTEM, "Rest flag tutorial resetted");
        }

        EditorGUILayout.EndVertical();
    }
}
#endif