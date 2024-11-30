using System.Text;
using Markdown.Converters;
using Markdown.Converters.Base;
using Markdown.Extensions;
using Markdown.Models;

namespace Markdown.Readers;

public class MarkdownToHtmlConverter
{
    private readonly HashSet<IConverter> converters =
    [
        new ItalicConverter(),
        new BoldConverter(),
        new HeaderConverter(),
    ];

    public string ConvertMarkdownToHtml(string text)
    {
        var tokens = new List<TagPair>();
        foreach (var converter in converters)
        {
            tokens.AddRange(converter.Convert(text).ToList());
        }
        var converterTokenPairs = tokens.FilterNonIntersectingTagPairs();
        var replacementTokens = FilterBoldTokens(converterTokenPairs)
            .FilterShielding(text);
        return RestoreToHtmlText(replacementTokens, text);
    }

    private IEnumerable<TagPair> FilterBoldTokens(ICollection<TagPair> tokenPairs)
    {
        var convertTokens = tokenPairs.ToDictionaryByType();
        if (!convertTokens.ContainsKey(typeof(BoldConverter))
            || !convertTokens.ContainsKey(typeof(ItalicConverter)))
        {
            return tokenPairs;
        }
        var boldTokens = convertTokens[typeof(BoldConverter)];
        var italicTokens = convertTokens[typeof(ItalicConverter)];
        convertTokens[typeof(BoldConverter)] = boldTokens.FilterOverlapping(italicTokens);
        var boldFilterTokens = convertTokens.SelectMany(x => x.Value);
        return boldFilterTokens;
    }

    private string RestoreToHtmlText(IEnumerable<TagToken> converterTokens, string originalText)
    {
        var resultBuilder = new StringBuilder(originalText);
        var replacements = converterTokens.OrderByDescending(r => r.TagIndex);
        foreach (var token in replacements)
        {
            if (token.TagIndex >= originalText.Length)
            {
                resultBuilder.AppendLine(token.ReplaceTag);
            }
            else
            {
                resultBuilder.Remove(token.TagIndex, token.SearchTag.Length);
                resultBuilder.Insert(token.TagIndex, token.ReplaceTag);
            }
        }
        return resultBuilder.ToString();
    }
}