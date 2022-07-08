using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TerrainTileSpawner))]
[CanEditMultipleObjects]
public class CustomEditorTerrainTileSpawner : Editor
{
    private SerializedProperty _tileWidthProperty;
    private SerializedProperty _tileSpeedProperty;
    private SerializedProperty _tileLifeTimeProperty;

    private TerrainTileSpawner _terrainTileSpawner;
    private float _speedUpMultiplier = 1.0f;
    private float _speedDownMultiplier = 1.0f;

    private void OnEnable()
    {
        _terrainTileSpawner = (TerrainTileSpawner)target;
        _tileWidthProperty = serializedObject.FindProperty("tileWidth");
        _tileSpeedProperty = serializedObject.FindProperty("tileSpeed");
        _tileLifeTimeProperty = serializedObject.FindProperty("tileLifeTime");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SpeedMultipliers();
        PropertiesValuesCheck();
    }

    private void PropertiesValuesCheck()
    {
        if(_tileWidthProperty.floatValue != TerrainTileSpawner.TILE_WIDTH)
            _tileWidthProperty.floatValue = TerrainTileSpawner.TILE_WIDTH;

        if (_tileSpeedProperty.floatValue < TerrainTileSpawner.MIN_TILE_SPEED)
            _tileSpeedProperty.floatValue = TerrainTileSpawner.MIN_TILE_SPEED;

        if (_tileSpeedProperty.floatValue > TerrainTileSpawner.MAX_TILE_SPEED)
            _tileSpeedProperty.floatValue = TerrainTileSpawner.MAX_TILE_SPEED;

        if (_tileLifeTimeProperty.floatValue < TerrainTileSpawner.MIN_TILE_LIFE_TIME)
            _tileLifeTimeProperty.floatValue = TerrainTileSpawner.MIN_TILE_LIFE_TIME;

        if (_tileLifeTimeProperty.floatValue > TerrainTileSpawner.MAX_TILE_LIFE_TIME)
            _tileLifeTimeProperty.floatValue = TerrainTileSpawner.MAX_TILE_LIFE_TIME;

        serializedObject.ApplyModifiedProperties();
    }

    private void SpeedMultipliers()
    {
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
