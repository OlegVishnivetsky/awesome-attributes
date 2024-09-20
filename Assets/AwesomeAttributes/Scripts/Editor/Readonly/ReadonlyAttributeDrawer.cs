using UnityEditor;
using UnityEngine;

namespace AwesomeAttributes.Editor
{
    /// <summary>
    /// Property drawer for Readonly attribute
    /// </summary>
    [CustomPropertyDrawer(typeof(ReadonlyAttribute))]
    public class ReadonlyAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ReadonlyAttribute readonlyAttribute = attribute as ReadonlyAttribute;
            bool isReadonly = false;

            switch (readonlyAttribute.ReadonlyType)
            {
                case ReadonlyType.Always:
                    isReadonly = true;
                    break;
                case ReadonlyType.InPlayMode:
                    isReadonly = Application.isPlaying;
                    break;

                default:
                    break;
            }

            DrawReadonlyField(position, property, label, isReadonly);
        }

        /// <summary>
        /// Draw readonly field
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        private static void DrawReadonlyField(Rect position, SerializedProperty property, 
            GUIContent label, bool isReadonly)
        {
            GUI.enabled = !isReadonly;
            EditorGUI.PropertyField(position, property, label);
            GUI.enabled = true;
        }
    }
}