using Markdown.Converters.Base;
using Markdown.Models;

namespace Markdown.Converters;

internal class BulletedListConverter : BaseMarkdownItemsConverter<ItemsConvertToken>
{
    protected override IEnumerable<ItemsConvertToken> Tokenize(string text)
    {
        throw new NotImplementedException();
    }
    
    protected override ItemsConvertToken ToHtmlToken(ItemsConvertToken markdownConvertToken)
    {
        throw new NotImplementedException();
    }

    protected override string ReconstructText(IEnumerable<ItemsConvertToken> tokens)
    {
        throw new NotImplementedException();
    }
}