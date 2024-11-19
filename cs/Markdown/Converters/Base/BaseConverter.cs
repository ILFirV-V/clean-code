using Markdown.Models;

namespace Markdown.Converters.Base;

public abstract class BaseConverter<TToken> : IConverter
    where TToken : ConvertToken
{
    public virtual string Convert(string text)
    {
        var startTokens = Tokenize(text);
        var htmlConvertedTokens = ApplyConversions(startTokens);
        return ReconstructText(htmlConvertedTokens);
    }

    protected virtual IEnumerable<TToken> ApplyConversions(IEnumerable<TToken> tokens)
    {
        foreach (var token in tokens)
        {
            yield return token.WithConvert ? ToHtmlToken(token) : token;
        }
    }
    
    protected abstract IEnumerable<TToken> Tokenize(string text);

    protected abstract TToken ToHtmlToken(TToken markdownConvertToken);

    protected abstract string ReconstructText(IEnumerable<TToken> tokens);
}