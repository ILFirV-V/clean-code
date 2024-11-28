using Markdown.Models;

namespace Markdown.Extensions;

public static class TagPairExtensions
{
    public static List<TagPair> FilterOverlapping(this IEnumerable<TagPair> tokens,
        ICollection<TagPair> overlappingTokens)
    {
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

    public static Dictionary<Type, List<TagPair>> ToDictionaryByType(this IEnumerable<TagPair> validTokens)
    {
        return validTokens
            .GroupBy(x => x.ConverterType)
            .ToDictionary(x => x.Key, x => x.ToList());
    }
}