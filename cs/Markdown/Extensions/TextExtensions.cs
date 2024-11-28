namespace Markdown.Extensions;

public static class TextExtensions
{
    public static bool IsInBounds(this string text, int index, int length)
    {
        return index >= 0 && index + length <= text.Length;
    }

    public static bool HasFollowingSpace(this string text, int index)
    {
        if (index < 0 || index >= text.Length)
        {
            return false;
        }

        return char.IsWhiteSpace(text[index]);
    }

    public static bool ContainsTagAtIndex(this string text, int index, string tagValue)
    {
        if (index < 0 || index + tagValue.Length > text.Length)
        {
            return false;
        }

        return text.Substring(index, tagValue.Length).Equals(tagValue);
    }

    public static bool HasAdjacentTag(this string text, int index, string tagValue)
    {
        if (index < 0 || index + tagValue.Length > text.Length)
        {
            return false;
        }

        return text.Substring(index, tagValue.Length).Equals(tagValue);
    }
}