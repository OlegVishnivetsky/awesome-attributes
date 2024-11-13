using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace AwesomeAttributes
{
    /// <summary>
    /// Handles all instances that use PlayerPrefs Attribute
    /// </summary>
    public class PlayerPrefsAttributeObserver : MonoBehaviour
    {
        private Dictionary<string, FieldInfo> onDisableFields = new();
        private Dictionary<string, FieldInfo> onDestroyFields = new();
        private List<MonoBehaviour> sceneObjects;

        private void Reset() => 
            gameObject.name = "PlayerPrefsAttributeObserver";

        private void Awake()
        {
            sceneObjects = FindObjectsOfType<MonoBehaviour>().ToList();
            InitializeDictionaries();
            LoadAllFields(onDisableFields);
            LoadAllFields(onDestroyFields);
        }

        private void OnDisable() => 
            SaveAllFields(onDisableFields);

        private void OnDestroy() => 
            SaveAllFields(onDestroyFields);

        private void OnApplicationQuit()
        {
            SaveAllFields(onDisableFields);
            SaveAllFields(onDestroyFields);
        }

        /// <summary>
        /// Initializes the dictionaries for fields marked with specific saving types.
        /// </summary>
        private void InitializeDictionaries()
        {
            onDisableFields = GetAllFieldsWithAttribute(PlayerPrefsAttributeSavingType.OnDisable);
            onDestroyFields = GetAllFieldsWithAttribute(PlayerPrefsAttributeSavingType.OnDestroy);
        }

        /// <summary>
        /// Iterates through all MonoBehaviour instances and saves the values of fields marked with a specific attribute to PlayerPrefs.
        /// </summary>
        /// <param name="fields">Dictionary of fields to save.</param>
        private void SaveAllFields(Dictionary<string, FieldInfo> fields)
        {
            foreach (var sceneObject in sceneObjects)
            {
                SaveFieldsForObject(sceneObject, fields);
            }
        }

        /// <summary>
        /// Iterates through all MonoBehaviour instances and loads the values of fields marked with a specific attribute from PlayerPrefs.
        /// </summary>
        /// <param name="fields">Dictionary of fields to load.</param>
        private void LoadAllFields(Dictionary<string, FieldInfo> fields)
        {
            foreach (var sceneObject in sceneObjects)
            {
                LoadFieldsForObject(sceneObject, fields);
            }
        }

        /// <summary>
        /// Saves fields for a specific object.
        /// </summary>
        /// <param name="obj">The object whose fields to save.</param>
        /// <param name="fields">Dictionary of fields to save.</param>
        private void SaveFieldsForObject(object obj, Dictionary<string, FieldInfo> fields)
        {
            foreach (KeyValuePair<string, FieldInfo> fieldEntry in fields)
            {
                FieldInfo fieldInfo = fieldEntry.Value;
                string fieldName = fieldEntry.Key;

                if (fieldInfo.DeclaringType != null && fieldInfo.DeclaringType.IsInstanceOfType(obj))
                {
                    object fieldValue = fieldInfo.GetValue(obj);

                    if (fieldValue != null)
                        SaveValue(fieldName, fieldValue);
                }
            }
        }

        /// <summary>
        /// Loads fields for a specific object.
        /// </summary>
        /// <param name="obj">The object whose fields to load.</param>
        /// <param name="fields">Dictionary of fields to load.</param>
        private void LoadFieldsForObject(object obj, Dictionary<string, FieldInfo> fields)
        {
            foreach (KeyValuePair<string, FieldInfo> fieldEntry in fields)
            {
                FieldInfo fieldInfo = fieldEntry.Value;
                string fieldName = fieldEntry.Key;

                if (fieldInfo.DeclaringType != null && fieldInfo.DeclaringType.IsInstanceOfType(obj))
                {
                    object loadedValue = LoadValue(fieldName, fieldInfo.FieldType);
                    if (loadedValue != null)
                    {
                        fieldInfo.SetValue(obj, loadedValue);
                    }
                }
            }
        }

        /// <summary>
        /// Saves a value to PlayerPrefs based on its type.
        /// </summary>
        /// <param name="key">Key for PlayerPrefs.</param>
        /// <param name="value">Value to save.</param>
        private void SaveValue(string key, object value)
        {
            switch (value)
            {
                case int intValue:
                    PlayerPrefs.SetInt(key, intValue);
                    break;
                case float floatValue:
                    PlayerPrefs.SetFloat(key, floatValue);
                    break;
                case string stringValue:
                    PlayerPrefs.SetString(key, stringValue);
                    break;
                case bool boolValue:
                    PlayerPrefs.SetInt(key, boolValue ? 1 : 0);
                    break;
            }

            PlayerPrefs.Save();
        }

        /// <summary>
        /// Loads a value from PlayerPrefs based on its type.
        /// </summary>
        /// <param name="key">Key for PlayerPrefs.</param>
        /// <param name="type">Type of the value to load.</param>
        /// <returns>Loaded value.</returns>
        private object LoadValue(string key, Type type)
        {
            if (!PlayerPrefs.HasKey(key)) return null;

            if (type == typeof(int))
                return PlayerPrefs.GetInt(key);
            if (type == typeof(float))
                return PlayerPrefs.GetFloat(key);
            if (type == typeof(string))
                return PlayerPrefs.GetString(key);
            if (type == typeof(bool))
                return PlayerPrefs.GetInt(key) == 1;

            return null;
        }

        /// <summary>
        /// Retrieves all fields marked with a specific attribute and saving type from all loaded assemblies.
        /// </summary>
        /// <param name="savingType">Type of saving attribute.</param>
        /// <returns>Dictionary of fields with the attribute.</returns>
        private Dictionary<string, FieldInfo> GetAllFieldsWithAttribute(PlayerPrefsAttributeSavingType savingType)
        {
            Dictionary<string, FieldInfo> fieldsWithAttribute = new();

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    IEnumerable<FieldInfo> fields = type
                        .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                        .Where(field => Attribute.IsDefined(field, typeof(PlayerPrefsAttribute)));

                    foreach (FieldInfo field in fields)
                    {
                        PlayerPrefsAttribute attribute = field.GetCustomAttribute<PlayerPrefsAttribute>();

                        if (attribute != null && attribute.SavingType == savingType)
                        {
                            fieldsWithAttribute.Add(attribute.Key, field);
                        }
                    }
                }
            }

            return fieldsWithAttribute;
        }
    }
}