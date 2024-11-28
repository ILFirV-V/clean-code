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

        var converterTokens = tokens
            .FilterNonIntersectingTagPairs()
            .ToDictionaryByType();
        converterTokens = FilterBoldTokens(converterTokens);
        var replacementTokens = converterTokens.SelectMany(x => x.Value);
        return RestoreToHtmlText(replacementTokens, text);
    }

    private Dictionary<Type, List<TagPair>> FilterBoldTokens(Dictionary<Type, List<TagPair>> convertTokens)
    {
        if (!convertTokens.ContainsKey(typeof(BoldConverter))
            || !convertTokens.ContainsKey(typeof(ItalicConverter)))
        {
            return convertTokens;
        }

        var boldTokens = convertTokens[typeof(BoldConverter)];
        var italicTokens = convertTokens[typeof(ItalicConverter)];
        convertTokens[typeof(BoldConverter)] = boldTokens.FilterOverlapping(italicTokens);
        return convertTokens;
    }

    private string RestoreToHtmlText(IEnumerable<TagPair> converterTokens, string originalText)
    {
        var resultBuilder = new StringBuilder(originalText);
        var replacements = converterTokens
            .SelectMany<TagPair, TagToken>(t => [t.OpenToken!, t.CloseToken!])
            .OrderByDescending(r => r.TagIndex);
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