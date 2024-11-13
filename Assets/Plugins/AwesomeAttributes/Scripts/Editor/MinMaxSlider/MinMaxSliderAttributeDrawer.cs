using UnityEditor;
using UnityEngine;

namespace AwesomeAttributes.Editor
{

    /// <summary>
    /// Custom property drawer for MinMaxSliderAttribute
    /// </summary>
    [CustomPropertyDrawer(typeof(MinMaxSliderAttribute))]
    public class MinMaxSliderAttributeDrawer : PropertyDrawer
    {
        private const int SplitAmount = 3;
        private const int FieldPadding = 40;
        private const int FieldSpace = 5;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            MinMaxSliderAttribute minMaxAttribute = attribute as MinMaxSliderAttribute;

            label.tooltip = $"Min: {minMaxAttribute.MinValue}. Max: {minMaxAttribute.MaxValue}";

            Rect controlRect = EditorGUI.PrefixLabel(position, label);

            Rect[] splittedRect = SplitRect(controlRect, SplitAmount);

            switch (property.propertyType)
            {
                case SerializedPropertyType.Vector2:
                    DrawVector2MinMaxSlider(property, minMaxAttribute, splittedRect);
                    break;
                case SerializedPropertyType.Vector2Int:
                    DrawVector2IntMinMaxSlider(property, minMaxAttribute, splittedRect);
                    break;
            }
        }

        /// <summary>
        /// Draws min/max slider for Vector2 struct
        /// </summary>
        /// <param name="property"></param>
        /// <param name="minMaxSliderAttribute"></param>
        /// <param name="splittedRect"></param>
        private void DrawVector2MinMaxSlider(SerializedProperty property,
            MinMaxSliderAttribute minMaxSliderAttribute, Rect[] splittedRect)
        {
            EditorGUI.BeginChangeCheck();

            Vector2 vector = property.vector2Value;

            float minVal = vector.x;
            float maxVal = vector.y;

            minVal = EditorGUI.FloatField(splittedRect[0], minVal);
            maxVal = EditorGUI.FloatField(splittedRect[2], maxVal);

            EditorGUI.MinMaxSlider(splittedRect[1], ref minVal, ref maxVal, minMaxSliderAttribute.MinValue, minMaxSliderAttribute.MaxValue);

            minVal = Mathf.Clamp(minVal, minMaxSliderAttribute.MinValue, minMaxSliderAttribute.MaxValue);
            maxVal = Mathf.Clamp(maxVal, minMaxSliderAttribute.MinValue, minMaxSliderAttribute.MaxValue);

            vector = new Vector2(Mathf.Min(minVal, maxVal), maxVal);

            if (EditorGUI.EndChangeCheck())
                property.vector2Value = vector;
        }

        /// <summary>
        /// Draws min/max slider for Vector2Int struct
        /// </summary>
        /// <param name="property"></param>
        /// <param name="minMaxSliderAttribute"></param>
        /// <param name="splittedRect"></param>
        private void DrawVector2IntMinMaxSlider(SerializedProperty property,
            MinMaxSliderAttribute minMaxSliderAttribute, Rect[] splittedRect)
        {
            EditorGUI.BeginChangeCheck();

            Vector2Int vector = property.vector2IntValue;
            float minVal = vector.x;
            float maxVal = vector.y;

            minVal = EditorGUI.FloatField(splittedRect[0], minVal);
            maxVal = EditorGUI.FloatField(splittedRect[2], maxVal);

            EditorGUI.MinMaxSlider(splittedRect[1], ref minVal, ref maxVal,
                minMaxSliderAttribute.MinValue, minMaxSliderAttribute.MaxValue);

            minVal = Mathf.Clamp(minVal, minMaxSliderAttribute.MinValue, minMaxSliderAttribute.MaxValue);
            maxVal = Mathf.Clamp(maxVal, minMaxSliderAttribute.MinValue, minMaxSliderAttribute.MaxValue);

            vector = new Vector2Int(Mathf.FloorToInt(Mathf.Min(minVal, maxVal)), Mathf.FloorToInt(maxVal));

            if (EditorGUI.EndChangeCheck())
                property.vector2IntValue = vector;
        }

        /// <summary>
        /// Splits a rectangle into a certain number of parts 
        /// </summary>
        /// <param name="rectToSplit"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private Rect[] SplitRect(Rect rectToSplit, int amount)
        {
            Rect[] rects = new Rect[amount];

            for (int i = 0; i < amount; i++)
            {
                rects[i] = new Rect(rectToSplit.position.x + i * rectToSplit.width / amount,
                    rectToSplit.position.y, rectToSplit.width / amount, rectToSplit.height);
            }

            rects[0].width -= FieldPadding + FieldSpace;
            rects[2].width -= FieldPadding + FieldSpace;

            rects[1].x -= FieldPadding;
            rects[1].width += FieldPadding * 2;

            rects[2].x += FieldPadding + FieldSpace;

            return rects;
        }
    }
}