using UnityEngine;

namespace AwesomeAttributes
{
    /// <summary>
    /// Changes GUI color
    /// </summary>
    public class GUIColorAttribute : PropertyAttribute
    {
        public readonly Color Color;

        public GUIColorAttribute(int r, int g, int b)
        {
            Color = new Color(r / 255f, g / 255f, b / 255f, 1f);
        }

        public GUIColorAttribute(string colorHex)
        {
            Color guiColor = Color.white;

            if (!ColorUtility.TryParseHtmlString(colorHex, out guiColor))
            {
                Color = guiColor;
                return;
            }

            Color = guiColor;
        }
    }
}