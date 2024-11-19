using Markdown.Models;

namespace Markdown.Converters.Base;

public abstract class BaseMarkdownItemsConverter<TItemsToken> : BaseConverter<TItemsToken>
    where TItemsToken : ItemsConvertToken
{
}