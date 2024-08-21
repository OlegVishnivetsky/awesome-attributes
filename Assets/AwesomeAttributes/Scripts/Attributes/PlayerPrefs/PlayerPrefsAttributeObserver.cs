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
        private Dictionary<string, FieldInfo> onDisableFields = new Dictionary<string, FieldInfo>();
        private Dictionary<string, FieldInfo> onDestroyFields = new Dictionary<string, FieldInfo>();

        private void Reset()
        {
            gameObject.name = "PlayerPrefsAttributeObserver";
        }

        private void Start()
        {
            InitializeDictionaries();
        }

        private void OnDisable()
        {
            SaveAllFields(onDisableFields);
        }

        private void OnDestroy()
        {
            SaveAllFields(onDestroyFields);
        }

        private void OnApplicationQuit()
        {
            SaveAllFields(onDisableFields);
            SaveAllFields(onDestroyFields);
        }

        /// <summary>
        /// Initializes the dictionaries for fields marked with specific saving types 
        /// </summary>
        private void InitializeDictionaries()
        {
            onDisableFields = GetAllFieldsWithAttribute(PlayerPrefsAttributeSavingType.OnDisable);
            onDestroyFields = GetAllFieldsWithAttribute(PlayerPrefsAttributeSavingType.OnDestroy);
        }

        /// <summary>
        /// Iterates through all MonoBehaviour instances and saves
        /// the values of fields marked with a specific attribute to PlayerPrefs
        /// </summary>
        /// <param name="fields"></param>
        private void SaveAllFields(Dictionary<string, FieldInfo> fields)
        {
            foreach (MonoBehaviour sceneObject in FindObjectsOfType<MonoBehaviour>())
            {
                foreach (KeyValuePair<string, FieldInfo> fieldEntry in fields)
                {
                    FieldInfo fieldInfo = fieldEntry.Value;
                    string fieldName = fieldEntry.Key;

                    if (fieldInfo.DeclaringType != null &&
                        fieldInfo.DeclaringType.IsInstanceOfType(sceneObject))
                    {
                        object fieldValue = fieldInfo.GetValue(sceneObject);

                        if (fieldValue != null)
                            SaveValue(fieldName, fieldValue);
                    }
                }
            }
        }

        /// <summary>
        /// Saves a value to PlayerPrefs based on its type
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
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
        /// Retrieves all fields marked with a specific attribute and saving type from all loaded assemblies
        /// </summary>
        /// <param name="savingType"></param>
        /// <returns></returns>
        private Dictionary<string, FieldInfo> GetAllFieldsWithAttribute(PlayerPrefsAttributeSavingType savingType)
        {
            Dictionary<string, FieldInfo> fieldsWithAttribute = new Dictionary<string, FieldInfo>();

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