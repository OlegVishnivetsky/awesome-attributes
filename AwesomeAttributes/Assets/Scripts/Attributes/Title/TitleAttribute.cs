using UnityEngine;

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

    /// <summary>
    /// Constructor with default TitleTextAlignments
    /// </summary>
    /// <param name="title"></param>
    /// <param name="subTitle"></param>
    /// <param name="bold"></param>
    /// <param name="withSeparationLine"></param>
    public TitleAttribute(string title, string subTitle = null,
        bool bold = true, bool withSeparationLine = true)
    {
        Title = title;
        SubTitle = subTitle;
        Bold = bold;
        WithSeparationLine = withSeparationLine;
        TextAlignments = AttributesParameters.DefaultTitleTextAlignments;
    }

    /// <summary>
    /// Constructor with required TitleTextAlignments parameter
    /// </summary>
    /// <param name="title"></param>
    /// <param name="textAlignments"></param>
    /// <param name="subTitle"></param>
    /// <param name="bold"></param>
    /// <param name="withSeparationLine"></param>
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