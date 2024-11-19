using Markdown.Converters.Base;
using Markdown.Models;

namespace Markdown.Converters;

internal class PictureConverter : BaseMarkdownLinksConverter<LinkConvertToken>
{
    protected override IEnumerable<LinkConvertToken> Tokenize(string text)
    {
        throw new NotImplementedException();
    }
    
    protected override LinkConvertToken ToHtmlToken(LinkConvertToken markdownConvertToken)
    {
        throw new NotImplementedException();
    }

    protected override string ReconstructText(IEnumerable<LinkConvertToken> tokens)
    {
        throw new NotImplementedException();
    }
}