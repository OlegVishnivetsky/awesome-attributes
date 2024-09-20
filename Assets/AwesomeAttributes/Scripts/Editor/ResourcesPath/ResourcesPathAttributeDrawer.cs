using UnityEditor;
using UnityEngine;

namespace AwesomeAttributes.Editor
{
    /// <summary>
    /// Custom property drawer for ResourcesPath attribute
    /// </summary>
    [CustomPropertyDrawer(typeof(ResourcesPathAttribute))]
    public class ResourcesPathAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            if (property.propertyType == SerializedPropertyType.String)
            {
                DrawStringField(position, property, label);
            }

            EditorGUI.EndProperty();
        }

        /// <summary>
        /// Draws the string field and handles the asset selection
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        private void DrawStringField(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginChangeCheck();

            string path = property.stringValue;
            Object asset = Resources.Load(path);

            asset = EditorGUI.ObjectField(position, label, asset, typeof(Object), false);

            if (EditorGUI.EndChangeCheck())
            {
                UpdatePropertyWithSelectedAsset(property, asset);
            }
        }

        /// <summary>
        /// Updates the property value with the selected asset's relative path if it is within the Resources folder
        /// </summary>
        /// <param name="property"></param>
        /// <param name="asset"></param>
        private void UpdatePropertyWithSelectedAsset(SerializedProperty property, Object asset)
        {
            if (asset != null)
            {
                string assetPath = AssetDatabase.GetAssetPath(asset);

                if (assetPath.StartsWith("Assets/Resources/"))
                {
                    string relativePath = assetPath.Substring("Assets/Resources/".Length);
                    relativePath = System.IO.Path.ChangeExtension(relativePath, null); // Remove file extension
                    property.stringValue = relativePath;
                }
                else
                {
                    Debug.LogError("Selected asset is not in the Resources folder");
                    property.stringValue = "";
                }
            }
            else
            {
                property.stringValue = "";
            }
        }
    }
}