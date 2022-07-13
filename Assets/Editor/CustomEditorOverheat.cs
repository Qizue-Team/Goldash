using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerOverheat))]
[CanEditMultipleObjects]
public class CustomEditorOverheat : Editor
{
    private SerializedProperty _decreaseAmount;
    private SerializedProperty _increaseAmount;

    private PlayerOverheat _playerOverheat;

    private void OnEnable()
    {
        _playerOverheat = (PlayerOverheat)target;
        _decreaseAmount = serializedObject.FindProperty("heatDecreaseAmount");
        _increaseAmount = serializedObject.FindProperty("heatIncreaseAmount");
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PropertiesValuesCheck();
    }

    private void PropertiesValuesCheck()
    {
        if (_decreaseAmount.floatValue < 0)
            _decreaseAmount.floatValue = 0;
        if(_increaseAmount.floatValue < 0)
            _increaseAmount.floatValue = 0;
        if (_decreaseAmount.floatValue > 1)
            _decreaseAmount.floatValue = 1;
        if(_increaseAmount.floatValue > 1)
            _increaseAmount.floatValue = 1;

        serializedObject.ApplyModifiedProperties();
    }

}
