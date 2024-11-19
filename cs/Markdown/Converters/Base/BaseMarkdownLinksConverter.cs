using Markdown.Models;

namespace Markdown.Converters.Base;

public abstract class BaseMarkdownLinksConverter<TLinkToken> : BaseConverter<TLinkToken>
    where TLinkToken : LinkConvertToken
{
    
}