using System;

namespace AwesomeAttributes
{
    /// <summary>
    /// All fields marked with this attribute will
    /// be automatically saved and loaded in the OnDestroy or OnDisable methods
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class PlayerPrefsAttribute : Attribute
    {
        public readonly string Key;
        public readonly PlayerPrefsAttributeSavingType SavingType;

        public PlayerPrefsAttribute(string key, PlayerPrefsAttributeSavingType savingType
            = PlayerPrefsAttributeSavingType.OnDisable)
        {
            Key = key;
            SavingType = savingType;
        }
    }
}