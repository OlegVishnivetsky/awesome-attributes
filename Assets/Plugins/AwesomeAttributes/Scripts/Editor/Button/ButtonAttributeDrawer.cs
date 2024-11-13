using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AwesomeAttributes.Editor
{
    /// <summary>
    /// Custom property drawer for button attribute
    /// </summary>
    [CustomPropertyDrawer(typeof(ButtonAttribute))]
    public class ButtonAttributeDrawer : PropertyDrawer
    {
        private object[] methodParameters;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ButtonAttribute buttonAttribute = attribute as ButtonAttribute;
            DrawPropertyFieldAndButtonWithParameterBox(position, property, label, buttonAttribute);
        }

        /// <summary>
        /// Draws a property field and a button below that property, with parameter boxes for methods that require input
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        /// <param name="buttonAttribute"></param>
        private void DrawPropertyFieldAndButtonWithParameterBox(Rect position, SerializedProperty property,
            GUIContent label, ButtonAttribute buttonAttribute)
        {
            EditorGUI.PropertyField(position, property, label, true);
            EditorGUILayout.Space();

            Object targetObject = AttributesEditorHelper.GetTargetObject(property);
            MethodInfo method = AttributesEditorHelper.GetMethodInfo(buttonAttribute.MethodName, targetObject);

            if (method != null)
            {
                ParameterInfo[] parameters = method.GetParameters();

                if (parameters.Length > 0)
                {
                    if (methodParameters == null || methodParameters.Length != parameters.Length)
                    {
                        methodParameters = new object[parameters.Length];
                    }

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
                        {
                            methodParameters[i] = EditorGUILayout.IntField(paramName, methodParameters[i] != null ? (int)methodParameters[i] : 0);
                        }
                        else if (parameterType == typeof(float))
                        {
                            methodParameters[i] = EditorGUILayout.FloatField(paramName, methodParameters[i] != null ? (float)methodParameters[i] : 0f);
                        }
                        else if (parameterType == typeof(string))
                        {
                            methodParameters[i] = EditorGUILayout.TextField(paramName, methodParameters[i] as string);
                        }
                        else if (parameterType == typeof(bool))
                        {
                            methodParameters[i] = EditorGUILayout.Toggle(paramName, methodParameters[i] != null && (bool)methodParameters[i]);
                        }
                        else
                        {
                            EditorGUILayout.LabelField($"{paramName}: Unsupported parameter type ({parameterType.Name})");
                        }
                    }
                    
                    DrawButton(buttonAttribute, method, parameters, targetObject);
                    EditorGUILayout.EndVertical();
                }
                else
                {
                    DrawButton(buttonAttribute, method, parameters, targetObject);
                }
            }
            else
            {
                EditorGUILayout.HelpBox($"Method '{buttonAttribute.MethodName}' not found on target object.", MessageType.Warning);
            }
            
            EditorGUILayout.Space();
        }

        private void DrawButton(ButtonAttribute buttonAttribute, 
            MethodInfo method, 
            ParameterInfo[] parameters, 
            Object targetObject)
        {
            if (GUILayout.Button(buttonAttribute.Lable, GUILayout.Height(buttonAttribute.Height)))
            {
                try
                {
                    method.Invoke(targetObject, parameters.Length > 0 ? methodParameters : null);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error invoking method {method.Name}: {e.Message}");
                }
            }
        }
    }
}