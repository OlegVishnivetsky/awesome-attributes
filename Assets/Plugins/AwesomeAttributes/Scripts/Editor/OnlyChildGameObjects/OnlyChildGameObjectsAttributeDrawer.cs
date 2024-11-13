using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace AwesomeAttributes.Editor
{
    /// <summary>
    /// Draws the OnlyChildGameObjects attribute in the Unity Inspector
    /// </summary>
    [CustomPropertyDrawer(typeof(OnlyChildGameObjectsAttribute))]
    public class OnlyChildGameObjectsAttributeDrawer : PropertyDrawer
    {
        private const float PickButtonWidth = 50f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.ObjectReference)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            EditorGUI.BeginProperty(position, label, property);

            FieldInfo fieldInfo = GetFieldInfo(property);
            DrawPropertyField(position, property, label, fieldInfo);

            EditorGUI.EndProperty();
        }

        /// <summary>
        /// Retrieves the FieldInfo for the specified property
        /// </summary>
        /// <param name="property">The serialized property.</param>
        /// <returns>The FieldInfo for the property.</returns>
        private FieldInfo GetFieldInfo(SerializedProperty property)
        {
            return AttributesEditorHelper.GetFieldInfo(property.propertyPath,
                property.serializedObject.targetObject);
        }

        /// <summary>
        /// Draws the object field and picker button for the property
        /// </summary>
        /// <param name="position">The position of the property in the Inspector.</param>
        /// <param name="property">The serialized property being drawn.</param>
        /// <param name="label">The label of the property.</param>
        /// <param name="fieldInfo">The FieldInfo for the property.</param>
        private void DrawPropertyField(Rect position, SerializedProperty property,
            GUIContent label, FieldInfo fieldInfo)
        {
            Rect objectFieldRect = new Rect(position.x, position.y, position.width
                - PickButtonWidth, position.height);
            Rect pickerButtonRect = new Rect(position.x + position.width - PickButtonWidth,
                position.y, PickButtonWidth, position.height);

            EditorGUI.ObjectField(objectFieldRect, property, fieldInfo.FieldType, label);

            if (GUI.Button(pickerButtonRect, "Pick"))
            {
                ShowChildObjectPicker(property, fieldInfo);
            }
        }

        /// <summary>
        /// Shows the child object picker window and assigns the selected object to the property
        /// </summary>
        /// <param name="property">The serialized property.</param>
        /// <param name="fieldInfo">The FieldInfo for the property.</param>
        private void ShowChildObjectPicker(SerializedProperty property, FieldInfo fieldInfo)
        {
            Component targetComponent = property.serializedObject.targetObject as Component;

            if (targetComponent != null)
            {
                ChildObjectPickerWindow.ShowWindow(targetComponent.transform, fieldInfo,
                    (pickedObject) =>
                    {
                        object valueToSet = null;

                        if (fieldInfo.FieldType == typeof(GameObject))
                        {
                            valueToSet = pickedObject;
                        }
                        else if (typeof(Component).IsAssignableFrom(fieldInfo.FieldType))
                        {
                            valueToSet = pickedObject.GetComponent(fieldInfo.FieldType);
                        }

                        if (valueToSet != null)
                        {
                            fieldInfo.SetValue(property.serializedObject.targetObject,
                                valueToSet);
                            property.serializedObject.ApplyModifiedProperties();
                        }
                    });
            }
        }
    }
}