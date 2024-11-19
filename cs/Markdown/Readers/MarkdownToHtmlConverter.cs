using Markdown.Converters;
using Markdown.Converters.Base;

namespace Markdown.Readers;

public class MarkdownToHtmlConverter
{
    private readonly IList<IConverter> converters = new List<IConverter>()
    {
        new BoldConverter(),
        new ItalicsConverter(),
        new HeadlineConverter()
        //...
    };

    public string ConvertMarkdownToHtml(string text)
    {
        var convertText = text;
        foreach (var converter in converters)
        {
            convertText = converter.Convert(convertText);
        }
        return convertText;
    }
}