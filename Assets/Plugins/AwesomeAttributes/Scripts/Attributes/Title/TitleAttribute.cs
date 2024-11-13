using UnityEngine;

namespace AwesomeAttributes
{
    /// <summary>
    /// Draws a title and subtitle (optional)
    /// </summary>
    public class TitleAttribute : PropertyAttribute
    {
        public readonly string Title;
        public readonly string SubTitle;

        public readonly bool Bold;
        public readonly bool WithSeparationLine;

        public readonly TitleTextAlignments TextAlignments;

        public TitleAttribute(string title, string subTitle = null,
            bool bold = true, bool withSeparationLine = true)
        {
            Title = title;
            SubTitle = subTitle;
            Bold = bold;
            WithSeparationLine = withSeparationLine;
            TextAlignments = AttributesParameters.DefaultTitleTextAlignments;
        }

        public TitleAttribute(string title, TitleTextAlignments textAlignments, string subTitle = null,
            bool bold = true, bool withSeparationLine = true)
        {
            Title = title;
            SubTitle = subTitle;
            Bold = bold;
            WithSeparationLine = withSeparationLine;
            TextAlignments = textAlignments;
        }
    }
}