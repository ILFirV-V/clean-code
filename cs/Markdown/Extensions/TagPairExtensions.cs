using Markdown.Constants;
using Markdown.Models;

namespace Markdown.Extensions;

public static class TagPairExtensions
{
    public static List<TagPair> FilterOverlapping(this IEnumerable<TagPair> tokens,
        ICollection<TagPair> overlappingTokens)
    {
        if (tokens is null || overlappingTokens is null)
        {
            throw new ArgumentNullException(tokens is null ? nameof(tokens) : nameof(overlappingTokens));
        }

        var filteredTokens = new List<TagPair>();
        foreach (var token in tokens)
        {
            var isOverlapping = overlappingTokens.Any(overlappingToken => overlappingToken.IsContains(token));
            if (isOverlapping)
            {
                continue;
            }

            filteredTokens.Add(token);
        }

        return filteredTokens;
    }

    public static HashSet<TagPair> FilterNonIntersectingTagPairs(this IEnumerable<TagPair> tokens)
    {
        ArgumentNullException.ThrowIfNull(tokens);
        var validTokens = new HashSet<TagPair>();
        var orderTokens = tokens
            .OrderBy(x => x.OpenToken.TagIndex);
        foreach (var tagPair in orderTokens)
        {
            var intersects = false;
            foreach (var otherPair in validTokens)
            {
                if (!tagPair.IsIntersecting(otherPair)
                    && !otherPair.IsIntersecting(tagPair))
                {
                    continue;
                }

                intersects = true;
                validTokens.Remove(otherPair);
                break;
            }

            if (!intersects)
            {
                validTokens.Add(tagPair);
            }
        }

        return validTokens;
    }

    public static IDictionary<Type, IList<TagPair>> ToDictionaryByType(this IEnumerable<TagPair> validTokens)
    {
        ArgumentNullException.ThrowIfNull(validTokens);
        return validTokens
            .GroupBy(x => x.ConverterType)
            .ToDictionary(x => x.Key, IList<TagPair> (x) => x.ToList());
    }

    public static IEnumerable<TagToken> FilterShielding(this IEnumerable<TagPair> tokenPairs, string text)
    {
        if (tokenPairs is null || text is null)
        {
            throw new ArgumentNullException(tokenPairs is null ? nameof(tokenPairs) : nameof(text));
        }

        foreach (var tagPair in tokenPairs)
        {
            if (tagPair.OpenToken.TagIndex >= text.Length
                || tagPair.CloseToken.TagIndex >= text.Length)
            {
                throw new IndexOutOfRangeException(
                    $"The token tag index {tagPair} is too big for text of length {text.Length}.");
            }

            var shielding = FilterConstants.Shielding.ToString();

            if (IsTokenShielded(tagPair.OpenToken, text))
            {
                yield return CreateShieldingToken(tagPair.OpenToken, shielding);
                continue;
            }

            if (IsTokenShielded(tagPair.CloseToken, text))
            {
                yield return CreateShieldingToken(tagPair.CloseToken, shielding);
                continue;
            }

            if (IsBothBehindIndicesShielding(text, tagPair.OpenToken.TagIndex))
            {
                yield return CreateDoubleShieldingToken(tagPair.OpenToken, shielding);
            }

            if (IsBothBehindIndicesShielding(text, tagPair.CloseToken.TagIndex))
            {
                yield return CreateDoubleShieldingToken(tagPair.CloseToken, shielding);
            }

            yield return tagPair.OpenToken;
            yield return tagPair.CloseToken;
        }
    }

    private static TagToken CreateShieldingToken(TagToken token, string shielding)
    {
        return new TagToken(shielding, "", token.TagIndex - shielding.Length);
    }

    private static TagToken CreateDoubleShieldingToken(TagToken token, string shielding)
    {
        return new TagToken($"{shielding}{shielding}", shielding, token.TagIndex - shielding.Length * 2);
    }

    private static bool IsTokenShielded(TagToken token, string text)
    {
        return IsBehindOneIndexShielding(text, token.TagIndex)
               && !IsBothBehindIndicesShielding(text, token.TagIndex);
    }

    private static bool IsBothBehindIndicesShielding(string text, int tagIndex)
    {
        var behindTwoIndices = tagIndex - 2;
        var behindOneIndices = tagIndex - 1;
        return behindTwoIndices >= 0
               && IsBehindOneIndexShielding(text, tagIndex)
               && IsBehindOneIndexShielding(text, behindOneIndices);
    }

    private static bool IsBehindOneIndexShielding(string text, int tagIndex)
    {
        return tagIndex > 0 && text[tagIndex - 1].Equals(FilterConstants.Shielding);
    }
}