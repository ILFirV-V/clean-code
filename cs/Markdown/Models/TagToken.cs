namespace Markdown.Models;

public record TagToken
{
    public string SearchTag { get; init; }
    public string ReplaceTag { get; init; }
    public int TagIndex { get; init; }

    public TagToken(string searchTag, string replaceTag, int tagIndex)
    {
        SearchTag = searchTag;
        ReplaceTag = replaceTag;
        TagIndex = tagIndex;
    }
}