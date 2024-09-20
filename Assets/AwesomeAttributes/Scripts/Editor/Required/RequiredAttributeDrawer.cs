using UnityEngine;
using UnityEditor;

namespace AwesomeAttributes.Editor
{
    /// <summary>
    /// Drawer for required attribute
    /// </summary>
    [CustomPropertyDrawer(typeof(RequiredAttribute))]
    public class RequiredAttributeDrawer : PropertyDrawer
    {
        private const float HelpBoxHeight = 37f;
        private const float SpaceBetweenHelpBox = 2f;
        private const float DefaultPropertyHeight = 18f;

        private Rect propertyRect;
        private float totalHeight;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            propertyRect = new Rect(position.x, position.y, position.width, EditorGUI.GetPropertyHeight(property));
            HandlePropertyFieldDrawing(property, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return totalHeight;
        }

        /// <summary>
        /// Handles the drawing of the property field, determining if a help box is needed
        /// </summary>
        /// <param name="property"></param>
        /// <param name="label"></param>
        private void HandlePropertyFieldDrawing(SerializedProperty property, GUIContent label)
        {
            RequiredAttribute requiredAttribute = attribute as RequiredAttribute;

            if (property.objectReferenceValue == null)
            {
                DrawFieldWithHelpBox(property, label, requiredAttribute);
            }
            else
            {
                DrawFieldWithoutHelpBox(property, label);
            }
        }

        /// <summary>
        /// Draws the property field with a help box indicating that the field is required
        /// </summary>
        /// <param name="property"></param>
        /// <param name="label"></param>
        /// <param name="requiredAttribute"></param>
        private void DrawFieldWithHelpBox(SerializedProperty property, GUIContent label,
            RequiredAttribute requiredAttribute)
        {
            Rect helpBoxRect = new Rect(propertyRect.xMin, propertyRect.yMax + SpaceBetweenHelpBox,
                propertyRect.width, HelpBoxHeight);
            EditorGUI.PropertyField(propertyRect, property, label);

            totalHeight = propertyRect.height + HelpBoxHeight + SpaceBetweenHelpBox;

            string message = requiredAttribute.Message ?? $"Field \"{property.name}\" is required";
            int messageType = (int)requiredAttribute.MessageType;
            EditorGUI.HelpBox(helpBoxRect, message, (MessageType)messageType);
        }

        /// <summary>
        /// Draws the property field without a help box
        /// </summary>
        /// <param name="property"></param>
        /// <param name="label"></param>
        private void DrawFieldWithoutHelpBox(SerializedProperty property, GUIContent label)
        {
            totalHeight = DefaultPropertyHeight;
            EditorGUI.PropertyField(propertyRect, property, label);
        }
    }
}