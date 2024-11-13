using UnityEditor;
using UnityEngine;

namespace AwesomeAttributes.Editor
{
    [CustomPropertyDrawer(typeof(PreviewAttribute))]
    public class PreviewAttributeDrawer : PropertyDrawer
    {
        private bool foldoutState = false;

        private const float FoldoutWidth = 15f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);


            Rect foldoutRect = new Rect(position.x, position.y, FoldoutWidth,
                EditorGUIUtility.singleLineHeight);
            Rect labelRect = new Rect(position.x + FoldoutWidth, position.y,
                position.width, EditorGUIUtility.singleLineHeight);
            Rect fieldRect = new Rect(position.x + EditorGUIUtility.labelWidth, position.y,
                position.width - EditorGUIUtility.labelWidth,
                EditorGUIUtility.singleLineHeight);

            foldoutState = EditorGUI.Foldout(foldoutRect, foldoutState,
                GUIContent.none, true);

            EditorGUI.LabelField(labelRect, label);
            EditorGUI.PropertyField(fieldRect, property, GUIContent.none);

            if (foldoutState && property.objectReferenceValue != null)
            {
                Sprite sprite = property.objectReferenceValue as Sprite;

                if (sprite != null)
                {
                    Rect previewRect = new Rect(position.x, position.y
                        + EditorGUIUtility.singleLineHeight + 2, position.width, 70f);
                    float aspectRatio = sprite.rect.width / sprite.rect.height;
                    float previewHeight = Mathf.Min(previewRect.width
                        / aspectRatio, previewRect.height);
                    float previewWidth = previewHeight * aspectRatio;

                    Rect spriteRect = new Rect(
                        previewRect.x + (previewRect.width - previewWidth) / 2,
                        previewRect.y,
                        previewWidth,
                        previewHeight
                    );

                    EditorGUI.DrawPreviewTexture(spriteRect, sprite.texture);
                }
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float baseHeight = EditorGUIUtility.singleLineHeight;

            if (foldoutState)
            {
                baseHeight += 72f;
            }

            return baseHeight;
        }
    }
}