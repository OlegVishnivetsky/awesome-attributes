using UnityEditor;
using UnityEngine;

namespace AwesomeAttributes.Editor
{
    /// <summary>
    /// Separation line attribute drawer
    /// </summary>
    [CustomPropertyDrawer(typeof(SeparationLineAttribute))]
    public class SeparationLineAttributeDrawer : DecoratorDrawer
    {
        public override void OnGUI(Rect position)
        {
            SeparationLineAttribute separatorAttribute = attribute as SeparationLineAttribute;

            Rect separatorRect = new Rect(position.xMin, position.yMin
                + separatorAttribute.TopSpacing, position.width, separatorAttribute.Height);

            EditorGUI.DrawRect(separatorRect, AttributesParameters.SeparationLineColor);
        }

        /// <summary>
        /// Overridden method, that determines total height of separation line.
        /// Spacing value is the upper and lower offset from the line
        /// </summary>
        /// <returns></returns>
        public override float GetHeight()
        {
            SeparationLineAttribute separatorAttribute = attribute as SeparationLineAttribute;

            float separatorTotalHeight =
                separatorAttribute.TopSpacing // Top padding
                + separatorAttribute.Height // Line height
                + separatorAttribute.BottomSpacing; // Bottom padding

            return separatorTotalHeight;
        }
    }
}