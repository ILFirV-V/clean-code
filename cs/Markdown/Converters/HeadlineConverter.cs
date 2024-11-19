using System.Text;
using Markdown.Converters.Base;
using Markdown.Models;

namespace Markdown.Converters;

internal class HeadlineConverter: BaseConverter<ConvertToken>
{
    public string StartOriginalValue => "#";
    public string EndOriginalValue => "\n";
    public string StartConvertedValue => "<h1>";
    public string EndConvertedValue => "</h1>";

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
        var textBuilder = new StringBuilder();
        return "";
    }
}