using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace xPoke.Tools.AnimationCreator
{
#if UNITY_EDITOR
    public partial class AnimationCreator : EditorWindow
    {
        [SerializeField]
        private Sprite[] sprites;

        private string _animationName;
        private float _frameTime;

        SerializedObject serializedObject;
        SerializedProperty spritesSerialized;


        [MenuItem("Tools/Animation Creator")]
        public static void StartEditor()
        {
            AnimationCreator animationCreatorEditorWindow = EditorWindow.GetWindow<AnimationCreator>("Animation Creator Editor", true);
            animationCreatorEditorWindow.Show();
        }

        private void OnEnable()
        {
            serializedObject = new SerializedObject(this);
            spritesSerialized = serializedObject.FindProperty("sprites");
        }

        void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space();

            GUILayout.Label("Sprite Animation Creation", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Animation Name:");
            _animationName = EditorGUILayout.TextField(_animationName, GUILayout.ExpandWidth(false));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Frames Time:");
            _frameTime = EditorGUILayout.FloatField(_frameTime, GUILayout.ExpandWidth(false));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            serializedObject.Update();
            EditorGUILayout.PropertyField(spritesSerialized, true);
            serializedObject.ApplyModifiedProperties(); // Always at the end of OnGUI() methods

            if (GUILayout.Button("Create"))
            {
                if (string.IsNullOrEmpty(_animationName))
                {
                    EditorUtility.DisplayDialog("Animation Null or Empty", "Please enter a name for the animation - animation name can not be null or empty", "Confirm");
                }
                else
                {
                    AnimationClip animClip = new AnimationClip();
                    animClip.frameRate = 25; // FPS

                    EditorCurveBinding spriteBinding = new EditorCurveBinding();
                    spriteBinding.type = typeof(SpriteRenderer);
                    spriteBinding.path = "";
                    spriteBinding.propertyName = "m_Sprite"; // The name must be different from "Sprite"

                    if (_frameTime <= 0)
                    {
                        _frameTime = 25;
                    }

                    ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[sprites.Length];
                    for (int i = 0; i < (sprites.Length); i++)
                    {
                        spriteKeyFrames[i] = new ObjectReferenceKeyframe();
                        spriteKeyFrames[i].time = (i * _frameTime) / 25;
                        spriteKeyFrames[i].value = sprites[i];
                    }
                    AnimationUtility.SetObjectReferenceCurve(animClip, spriteBinding, spriteKeyFrames);

                    AssetDatabase.CreateAsset(animClip, "Assets/Animations/" + _animationName + ".anim");
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }

            EditorGUILayout.EndVertical();
        }
    }
#endif
}
