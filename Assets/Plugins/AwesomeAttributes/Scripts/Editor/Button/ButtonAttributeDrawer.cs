using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace AwesomeAttributes.Editor
{
    [CustomPropertyDrawer(typeof(ButtonAttribute))]
    public class ButtonAttributeDrawer : PropertyDrawer
    {
        private object[] methodParameters;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ButtonAttribute buttonAttribute = attribute as ButtonAttribute;
            DrawPropertyFieldAndButtonWithParameterBox(position, property, label, buttonAttribute);
        }

        private void DrawPropertyFieldAndButtonWithParameterBox(Rect position, SerializedProperty property,
            GUIContent label, ButtonAttribute buttonAttribute)
        {
            EditorGUI.PropertyField(position, property, label, true);
            EditorGUILayout.Space();

            object targetObject = GetNestedTargetObject(property);
            
            if (targetObject == null)
            {
                EditorGUILayout.HelpBox($"Could not resolve target object for {property.propertyPath}.", MessageType.Warning);
                return;
            }

            MethodInfo method = GetMethodInfo(buttonAttribute.MethodName, targetObject);
            if (method != null)
            {
                ParameterInfo[] parameters = method.GetParameters();

                if (parameters.Length > 0)
                {
                    if (methodParameters == null || methodParameters.Length != parameters.Length)
                        methodParameters = new object[parameters.Length];

                    GUIStyle boxStyle = new GUIStyle("window");
                    boxStyle.padding = new RectOffset(15, 15, 10, 10);

                    GUIStyle labelStyle = new GUIStyle(EditorStyles.label);
                    labelStyle.fontStyle = FontStyle.Bold;
                    labelStyle.normal.textColor = EditorStyles.label.normal.textColor;
                    labelStyle.fontSize = 14;

                    EditorGUILayout.BeginVertical(boxStyle);
                    EditorGUILayout.LabelField($"Method {method.Name}(...)", labelStyle);

                    for (int i = 0; i < parameters.Length; i++)
                    {
                        ParameterInfo parameter = parameters[i];
                        Type parameterType = parameter.ParameterType;
                        string paramName = ObjectNames.NicifyVariableName(parameter.Name);

                        if (parameterType == typeof(int))
                            methodParameters[i] = EditorGUILayout.IntField(paramName, methodParameters[i] != null ? (int)methodParameters[i] : 0);
                        else if (parameterType == typeof(float))
                            methodParameters[i] = EditorGUILayout.FloatField(paramName, methodParameters[i] != null ? (float)methodParameters[i] : 0f);
                        else if (parameterType == typeof(string))
                            methodParameters[i] = EditorGUILayout.TextField(paramName, methodParameters[i] as string);
                        else if (parameterType == typeof(bool))
                            methodParameters[i] = EditorGUILayout.Toggle(paramName, methodParameters[i] != null && (bool)methodParameters[i]);
                        else
                            EditorGUILayout.LabelField($"{paramName}: Unsupported parameter type ({parameterType.Name})");
                    }

                    DrawButton(buttonAttribute, method, parameters, targetObject);
                    EditorGUILayout.EndVertical();
                }
                else
                    DrawButton(buttonAttribute, method, parameters, targetObject);
            }
            else
                EditorGUILayout.HelpBox($"Method '{buttonAttribute.MethodName}' not found on target object of type {targetObject.GetType().Name} for {property.propertyPath}.", MessageType.Warning);

            EditorGUILayout.Space();
        }

        private void DrawButton(ButtonAttribute buttonAttribute, MethodInfo method, ParameterInfo[] parameters, object targetObject)
        {
            if (GUILayout.Button(buttonAttribute.Lable, GUILayout.Height(buttonAttribute.Height)))
            {
                try
                {
                    method.Invoke(targetObject, parameters.Length > 0 ? methodParameters : null);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error invoking method {method.Name} on {targetObject.GetType().Name}: {e.Message}");
                }
            }
        }

        private object GetNestedTargetObject(SerializedProperty property)
        {
            object target = property.serializedObject.targetObject;
            string[] pathParts = property.propertyPath.Split('.');

            for (int i = 0; i < pathParts.Length - 1; i++)
            {
                if (pathParts[i] == "Array" && i + 1 < pathParts.Length && pathParts[i + 1].StartsWith("data["))
                {
                    string arrayPath = string.Join(".", pathParts.Take(i));
                    int indexStart = pathParts[i + 1].IndexOf('[') + 1;
                    int indexEnd = pathParts[i + 1].IndexOf(']');
                    
                    if (int.TryParse(pathParts[i + 1].Substring(indexStart, indexEnd - indexStart), out int index))
                    {
                        target = GetArrayElementAtIndex(arrayPath, index, target, property);
                        if (target == null)
                        {
                            Debug.LogWarning($"Failed to resolve array element at index {index} for path {arrayPath}");
                            return null;
                        }
                    }
                    
                    i++;
                    continue;
                }

                FieldInfo fieldInfo = target.GetType().GetField(pathParts[i], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                
                if (fieldInfo != null)
                {
                    target = fieldInfo.GetValue(target);
                    
                    if (target == null)
                    {
                        Debug.LogWarning($"Field {pathParts[i]} is null in path {property.propertyPath}");
                        return null;
                    }
                }
                else
                {
                    Debug.LogWarning($"Field {pathParts[i]} not found in type {target.GetType().Name} for path {property.propertyPath}");
                    return null;
                }
            }

            return target;
        }

        private object GetArrayElementAtIndex(string arrayPath, int index, object target, SerializedProperty property)
        {
            FieldInfo fieldInfo = target.GetType().GetField(arrayPath, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            
            if (fieldInfo != null)
                target = fieldInfo.GetValue(target);
            else
            {
                Debug.LogWarning($"Array field {arrayPath} not found in type {target.GetType().Name}");
                return null;
            }

            if (target is Array array && index < array.Length)
                return array.GetValue(index);
            
            if (target is System.Collections.IList list && index < list.Count)
                return list[index];
            
            Debug.LogWarning($"Array or list at {arrayPath} is invalid or index {index} is out of bounds");
            return null;
        }

        private MethodInfo GetMethodInfo(string methodName, object targetObject)
        {
            if (targetObject == null) return null;

            return targetObject.GetType().GetMethod(methodName,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            EditorGUI.GetPropertyHeight(property, label, true);
    }
}