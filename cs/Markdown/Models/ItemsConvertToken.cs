namespace Markdown.Models;

public record ItemsConvertToken : ConvertToken
{
    public ItemsConvertToken(int startPosition, int endPosition, string content, bool withConvert) 
        : base(startPosition, endPosition, content, withConvert)
    {
    }
}