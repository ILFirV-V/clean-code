namespace Markdown.Models;

public record LinkConvertToken : ConvertToken
{
    public LinkConvertToken(int startPosition, int endPosition, string content, bool withConvert)
        : base(startPosition, endPosition, content, withConvert)
    {
    }
}