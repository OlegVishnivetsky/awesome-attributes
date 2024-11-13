using UnityEditor;
using UnityEngine;

namespace AwesomeAttributes.Editor
{
    /// <summary>
    /// Title attribute drawer
    /// </summary>
    [CustomPropertyDrawer(typeof(TitleAttribute))]
    public class TitleAttributeDrawer : DecoratorDrawer
    {
        private bool isSubTitleUsed = false;
        private TextAnchor textAnchor;

        private const float WithSubTitleHeight = 40f;
        private const float OnlyTitleHeight = 25f;
        private const float SeparationLineTopPadding = -4f;

        public override void OnGUI(Rect position)
        {
            TitleAttribute titleAttributes = attribute as TitleAttribute;

            GUIStyle mainTitleStyle = SetUpTitleStyle(titleAttributes.Bold);
            GUIStyle subTitleStyle = SetUpSubTitleStyle();

            DrawTitle(titleAttributes.Title, mainTitleStyle);

            isSubTitleUsed = false;
            textAnchor = GetLableTextAnchor(titleAttributes);

            if (titleAttributes.SubTitle != null)
            {
                isSubTitleUsed = true;

                DrawTitle(titleAttributes.SubTitle, subTitleStyle);
            }

            DrawSeparationLine(titleAttributes, position);
        }

        /// <summary>
        /// Overridden method, that determines what height the title field will have.
        /// If you want to change the title font size, you will most likely have 
        /// to change these two height constants
        /// </summary>
        /// <returns></returns>
        public override float GetHeight()
        {
            if (isSubTitleUsed)
            {
                return WithSubTitleHeight;
            }
            else
            {
                return OnlyTitleHeight;
            }
        }

        /// <summary>
        /// Setting up title style
        /// </summary>
        /// <param name="bold"></param>
        /// <returns></returns>
        private GUIStyle SetUpTitleStyle(bool bold)
        {
            GUIStyle mainTitleStyle = new GUIStyle();
            mainTitleStyle.fontSize = AttributesParameters.TitleFontSize;
            mainTitleStyle.alignment = textAnchor;
            mainTitleStyle.normal.textColor = new Color(11f, 11f, 11f, 1f);
            HandleTitleFontStyle();

            return mainTitleStyle;

            void HandleTitleFontStyle()
            {
                if (bold)
                    mainTitleStyle.fontStyle = FontStyle.Bold;
                else
                    mainTitleStyle.fontStyle = FontStyle.Normal;
            }
        }

        /// <summary>
        /// Setting up sub title style
        /// </summary>
        /// <returns></returns>
        private GUIStyle SetUpSubTitleStyle()
        {
            GUIStyle subTitleStyle = new GUIStyle();
            subTitleStyle.fontSize = AttributesParameters.SubTitleFontSize;
            subTitleStyle.alignment = textAnchor;
            subTitleStyle.normal.textColor = Color.gray;
            subTitleStyle.padding.top = -5;

            return subTitleStyle;
        }

        /// <summary>
        /// Draws separator line after title 
        /// </summary>
        /// <param name="titleAttributes"></param>
        /// <param name="position"></param>
        private void DrawSeparationLine(TitleAttribute titleAttributes, Rect position)
        {
            if (titleAttributes.WithSeparationLine)
            {
                Rect separatorRect = new Rect(position.xMin, position.yMax + SeparationLineTopPadding,
                position.width, 1f);

                EditorGUI.DrawRect(separatorRect, AttributesParameters.SeparationLineColor);
            }
        }

        /// <summary>
        /// Draws lable field
        /// </summary>
        /// <param name="text"></param>
        /// <param name="style"></param>
        private void DrawTitle(string text, GUIStyle style)
        {
            EditorGUILayout.LabelField(text, style);
        }

        /// <summary>
        /// Returns TextAnchor depending on the title TextAlignments
        /// </summary>
        /// <param name="titleAttributes"></param>
        /// <returns></returns>
        private TextAnchor GetLableTextAnchor(TitleAttribute titleAttributes)
        {
            switch (titleAttributes.TextAlignments)
            {
                case TitleTextAlignments.Left:
                    return TextAnchor.MiddleLeft;

                case TitleTextAlignments.Center:
                    return TextAnchor.MiddleCenter;

                case TitleTextAlignments.Right:
                    return TextAnchor.MiddleRight;

                default:
                    return TextAnchor.MiddleLeft;
            }
        }
    }
}