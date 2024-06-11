using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;
using System.Reflection;

public class ChildObjectPickerWindow : EditorWindow
{
    private Action<GameObject> onPicked;
    private List<GameObject> childObjects = new List<GameObject>();
    private Vector2 scrollPosition;

    private const int WindowWidth = 300;
    private const float MinWindowHeight = 70f;

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Select a Child GameObject", 
            EditorStyles.boldLabel);
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        foreach (var child in childObjects)
        {
            EditorGUILayout.BeginHorizontal();

            GUILayout.Box("", GUILayout.Width(300), GUILayout.Height(30));
            Rect lastRect = GUILayoutUtility.GetLastRect();

            GUI.Box(lastRect, GUIContent.none);
            GUI.Label(new Rect(lastRect.x + 5, lastRect.y + 5, 200, 20), 
                child.name);

            if (GUI.Button(new Rect(lastRect.x + 210, lastRect.y + 5, 80, 
                20), 
                "Assign"))
            {
                onPicked?.Invoke(child);
                Close();
            }

            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);
        }

        EditorGUILayout.EndScrollView();
    }

    public static void ShowWindow(Transform parentTransform, FieldInfo fieldInfo, 
        Action<GameObject> onPicked)
    {
        ChildObjectPickerWindow window = GetWindow<ChildObjectPickerWindow>(true,
            "Select Child Object", true);

        window.onPicked = onPicked;
        window.childObjects.Clear();

        for (int i = 0; i < parentTransform.childCount; i++)
        {
            if (parentTransform.GetChild(i).GetComponent(fieldInfo.FieldType.Name))
                window.childObjects.Add(parentTransform.GetChild(i).gameObject);
        }

        float windowHeight = Mathf.Min(600, 65 + 45 * window.childObjects.Count);
        window.position = new Rect(Screen.width / 2, Screen.height / 2, WindowWidth,
            windowHeight);
        window.maxSize = new Vector2(WindowWidth, windowHeight);
        window.minSize = new Vector2(WindowWidth, MinWindowHeight);

        window.Show();
    }
}