using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace AwesomeAttributes.Editor
{
    [CustomPropertyDrawer(typeof(SceneAttribute))]
    public class SceneAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    DrawIntField(position, property, label);
                    break;
                case SerializedPropertyType.String:
                    DrawStringField(position, property, label);
                    break;
                case SerializedPropertyType.ObjectReference:
                    if (property.objectReferenceValue == null ||
                        property.objectReferenceValue is SceneAsset)
                        DrawSceneAssetField(position, property, label);

                    break;
                default:
                    EditorGUI.LabelField(position, label.text,
                        "Use [Scene] with int, string, or SceneAsset");
                    break;
            }

            EditorGUI.EndProperty();
        }

        private void DrawIntField(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
            string[] sceneNames = new string[scenes.Length];

            for (int i = 0; i < scenes.Length; i++)
            {
                sceneNames[i] = Path.GetFileNameWithoutExtension(scenes[i].path);
            }

            property.intValue = EditorGUI.Popup(position, label.text,
                property.intValue, sceneNames);
        }

        private void DrawStringField(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
            string[] sceneNames = new string[scenes.Length];

            for (int i = 0; i < scenes.Length; i++)
            {
                sceneNames[i] = Path.GetFileNameWithoutExtension(scenes[i].path);
            }

            int selectedIndex = Mathf.Max(0, Array.IndexOf(sceneNames,
                property.stringValue));
            selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex,
                sceneNames);

            if (sceneNames.Length > 0)
                property.stringValue = sceneNames[selectedIndex];
        }

        private void DrawSceneAssetField(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginChangeCheck();
            SceneAsset sceneAsset = (SceneAsset)EditorGUI.ObjectField(position, label,
                property.objectReferenceValue, typeof(SceneAsset), false);

            if (EditorGUI.EndChangeCheck())
            {
                property.objectReferenceValue = sceneAsset;
            }
        }
    }
}