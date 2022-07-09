using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(PlatformSpawner))]
[CanEditMultipleObjects]
public class CustomEditorPlatformSpawner : Editor
{
    private SerializedProperty _levelNumbersProperty;
    private SerializedProperty _spaceBetweenPlatformsProperty;
    private SerializedProperty _offeset;

    private PlatformSpawner _platformSpawner;

    private void OnEnable()
    {
        _platformSpawner = (PlatformSpawner)target;
        _levelNumbersProperty = serializedObject.FindProperty("levelNumbers");
        _spaceBetweenPlatformsProperty = serializedObject.FindProperty("spaceBetweenPlatforms");
        _offeset = serializedObject.FindProperty("offset");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PropertiesValuesCheck();
    }

    private void PropertiesValuesCheck()
    {
        if(_levelNumbersProperty.intValue < PlatformSpawner.MIN_LEVELS_NUMBER)
            _levelNumbersProperty.intValue = PlatformSpawner.MIN_LEVELS_NUMBER;

        if (_spaceBetweenPlatformsProperty.floatValue < PlatformSpawner.MIN_SPACE_BETWEEN_PLATFORMS)
            _spaceBetweenPlatformsProperty.floatValue = PlatformSpawner.MIN_SPACE_BETWEEN_PLATFORMS;

        if(_offeset.floatValue < PlatformSpawner.MIN_OFFSET)
            _offeset.floatValue = PlatformSpawner.MIN_OFFSET;

        serializedObject.ApplyModifiedProperties();
    }
}
