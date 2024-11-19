using System.Text;
using Markdown.Converters.Base;
using Markdown.Models;

namespace Markdown.Converters;

internal class ItalicsConverter : BaseConverter<ConvertToken>
{
    public string StartOriginalValue => "__";
    public string EndOriginalValue => "__";
    public string StartConvertedValue => "<strong>";
    public string EndConvertedValue => "</strong>";

    protected override IEnumerable<ConvertToken> Tokenize(string text)
    {
        var contentBuilder = new StringBuilder();
        yield return new ConvertToken(0,0,"",true);
    }

    protected override ConvertToken ToHtmlToken(ConvertToken markdownConvertToken)
    {
        return new ConvertToken(0,0,"",false);
    }

    protected override string ReconstructText(IEnumerable<ConvertToken> tokens)
    {
        return "";
    }
}