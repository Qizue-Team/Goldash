using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TerrainTileSpawner))]
[CanEditMultipleObjects]
public class CustomEditorTerrainTileSpawner : Editor
{
    private TerrainTileSpawner _terrainTileSpawner;
    private float _speedUpMultiplier = 1.0f;
    private float _speedDownMultiplier = 1.0f;

    private void OnEnable()
    {
        _terrainTileSpawner = (TerrainTileSpawner)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Speed Modifier", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Speed Up Multiplier:");
        _speedUpMultiplier = EditorGUILayout.FloatField(_speedUpMultiplier, GUILayout.Width(305));
        EditorGUILayout.EndHorizontal();
        //EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Speed Down Multiplier:");
        _speedDownMultiplier = EditorGUILayout.FloatField(_speedDownMultiplier, GUILayout.Width(305));
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        if (GUILayout.Button("Speed Up"))
        {
            _terrainTileSpawner.SpeedUp(_speedUpMultiplier);
        }
        
        EditorGUILayout.Space();

        if (GUILayout.Button("Speed Down"))
        {
            _terrainTileSpawner.SpeedDown(_speedDownMultiplier);
        }
    }
}
