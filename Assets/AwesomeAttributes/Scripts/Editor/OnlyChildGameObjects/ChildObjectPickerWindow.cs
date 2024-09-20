using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;
using System.Reflection;

namespace AwesomeAttributes.Editor
{
    /// <summary>
    /// Custom object picker editor window
    /// </summary>
    public class ChildObjectPickerWindow : EditorWindow
    {
        private const int WindowWidth = 300;
        private const float MinWindowHeight = 70f;
        private const float MaxWindowHeight = 600f;
        private const float ItemHeight = 35f;
        private const float LabelOffsetX = 5f;
        private const float LabelOffsetY = 5f;
        private const float ButtonOffsetX = 210f;
        private const float ButtonWidth = 80f;
        private const float ItemSpacing = 5f;

        private Action<GameObject> onPicked;
        private List<GameObject> childObjects = new List<GameObject>();
        private Vector2 scrollPosition;

        private void OnGUI()
        {
            DrawHeader();
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            foreach (GameObject child in childObjects)
            {
                DrawChildItem(child);
            }

            EditorGUILayout.EndScrollView();
        }

        /// <summary>
        /// Draws the header lable field
        /// </summary>
        private void DrawHeader()
        {
            EditorGUILayout.LabelField("Select a Child GameObject", EditorStyles.boldLabel);
        }

        /// <summary>
        /// Draws child items with label, background box and assign button
        /// </summary>
        /// <param name="child"></param>
        private void DrawChildItem(GameObject child)
        {
            EditorGUILayout.BeginHorizontal();

            GUILayout.Box("", GUILayout.Width(WindowWidth), GUILayout.Height(ItemHeight));
            Rect lastRect = GUILayoutUtility.GetLastRect();

            DrawBackgroundBox(lastRect);
            DrawLabel(lastRect, child.name);
            DrawAssignButton(lastRect, child);

            EditorGUILayout.EndHorizontal();
            GUILayout.Space(ItemSpacing);
        }

        /// <summary>
        /// Creates a background box 
        /// </summary>
        /// <param name="rect"></param>
        private void DrawBackgroundBox(Rect rect)
        {
            GUI.Box(rect, GUIContent.none);
        }

        /// <summary>
        /// Draws item label
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="label"></param>
        private void DrawLabel(Rect rect, string label)
        {
            GUI.Label(new Rect(rect.x + LabelOffsetX, rect.y + LabelOffsetY,
                200, 20), label);
        }

        /// <summary>
        /// Draws assign button
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="child"></param>
        private void DrawAssignButton(Rect rect, GameObject child)
        {
            if (GUI.Button(new Rect(rect.x + ButtonOffsetX, rect.y + LabelOffsetY,
                ButtonWidth, 20), "Assign"))
            {
                onPicked?.Invoke(child);
                Close();
            }
        }

        /// <summary>
        /// Show object picker window
        /// </summary>
        /// <param name="parentTransform"></param>
        /// <param name="fieldInfo"></param>
        /// <param name="onPicked"></param>
        public static void ShowWindow(Transform parentTransform, FieldInfo fieldInfo, Action<GameObject> onPicked)
        {
            ChildObjectPickerWindow window = GetWindow<ChildObjectPickerWindow>(true,
                "Select Child Object", true);

            window.onPicked = onPicked;
            window.childObjects = GetChildObjects(parentTransform, fieldInfo.FieldType);
            window.AdjustWindowSize();

            window.Show();
        }

        /// <summary>
        /// Gets child objects of the same type as the field 
        /// </summary>
        /// <param name="parentTransform"></param>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        private static List<GameObject> GetChildObjects(Transform parentTransform, 
            Type fieldType)
        {
            List<GameObject> childObjects = new List<GameObject>();

            for (int i = 0; i < parentTransform.childCount; i++)
            {
                Transform child = parentTransform.GetChild(i);

                if (fieldType == typeof(GameObject))
                {
                    childObjects.Add(child.gameObject);
                }
                else if (typeof(Component).IsAssignableFrom(fieldType))
                {
                    if (child.GetComponent(fieldType) != null)
                    {
                        childObjects.Add(child.gameObject);
                    }
                }
            }

            return childObjects;
        }

        /// <summary>
        /// Sets the window size depending on the number of child objects
        /// </summary>
        private void AdjustWindowSize()
        {
            float windowHeight = Mathf.Clamp(ItemHeight * childObjects.Count + ItemSpacing
                * (childObjects.Count - 1) + 50f, MinWindowHeight, MaxWindowHeight);
            position = new Rect(Screen.width / 2, Screen.height / 2, WindowWidth, windowHeight);
            minSize = new Vector2(WindowWidth, MinWindowHeight);
            maxSize = new Vector2(WindowWidth, MaxWindowHeight);
        }
    }
}