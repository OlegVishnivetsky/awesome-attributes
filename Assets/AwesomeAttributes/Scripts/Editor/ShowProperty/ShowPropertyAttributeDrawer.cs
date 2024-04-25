using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ShowPropertyAttribute))]
public class ShowPropertyAttributeDrawer : PropertyDrawer
{
    private PropertyInfo propertyFieldInfo = null;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        UnityEngine.Object target = property.serializedObject.targetObject;

        if (propertyFieldInfo == null)
            propertyFieldInfo = target.GetType()
                .GetProperty(((ShowPropertyAttribute)attribute).PropertyName,
                                                 BindingFlags.Instance 
                                                 | BindingFlags.Public 
                                                 | BindingFlags.NonPublic);

        if (propertyFieldInfo != null)
        {
            object value = propertyFieldInfo.GetValue(target, null);

            EditorGUI.BeginChangeCheck();

            value = AttributesHelper.DrawPropertyByType(position, property.propertyType,
                propertyFieldInfo.PropertyType, value, label);

            if (EditorGUI.EndChangeCheck() && propertyFieldInfo != null)
            {
                Undo.RecordObject(target, "Inspector");
                propertyFieldInfo.SetValue(target, value, null);
            }

        }
        else
        {
            EditorGUI.LabelField(position, "Error: could not retrieve property.");
        }
    }
}