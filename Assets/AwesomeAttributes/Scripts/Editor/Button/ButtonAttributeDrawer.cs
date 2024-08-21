using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace AwesomeAttributes
{
    /// <summary>
    /// Custom property drawer for button attribute
    /// </summary>
    [CustomPropertyDrawer(typeof(ButtonAttribute))]
    public class ButtonAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ButtonAttribute buttonAttribute = attribute as ButtonAttribute;
            DrawPropertyFieldAndButton(position, property, label, buttonAttribute);
        }

        /// <summary>
        /// Draws a property field and a button below that property
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        /// <param name="buttonAttribute"></param>
        private static void DrawPropertyFieldAndButton(Rect position, SerializedProperty property,
            GUIContent label, ButtonAttribute buttonAttribute)
        {
            Object targetObject = AttributesEditorHelper.GetTargetObject(property);
            MethodInfo method = AttributesEditorHelper.GetMethodInfo(buttonAttribute.MethodName, targetObject);

            EditorGUI.PropertyField(position, property, label);

            if (GUILayout.Button(buttonAttribute.Lable,
                GUILayout.Height(buttonAttribute.Height)))
            {
                method.Invoke(targetObject, null);
            }
        }
    }
}