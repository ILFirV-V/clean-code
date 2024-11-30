namespace Markdown.Models;

public record TagPair
{
    public Type ConverterType { get; private init; }
    public TagToken OpenToken { get; private init; }
    public TagToken CloseToken { get; private init; }

    public bool IsIntersecting(TagPair otherTagPair)
    {
        return OpenToken.TagIndex < otherTagPair.OpenToken.TagIndex
               && otherTagPair.OpenToken.TagIndex < CloseToken.TagIndex
               && otherTagPair.OpenToken.TagIndex < CloseToken.TagIndex
               && CloseToken.TagIndex < otherTagPair.CloseToken.TagIndex;
    }

    public bool IsContains(TagPair otherTagPair)
    {
        return OpenToken.TagIndex < otherTagPair.OpenToken.TagIndex
               && otherTagPair.OpenToken.TagIndex < CloseToken.TagIndex
               && OpenToken.TagIndex < otherTagPair.CloseToken.TagIndex
               && otherTagPair.CloseToken.TagIndex < CloseToken.TagIndex;
    }

    public static TagPairBuilder CreateBuilder(Type converterType)
    {
        var tagPairBuilder = new TagPairBuilder();
        tagPairBuilder.AddConverterType(converterType);
        return tagPairBuilder;
    }

    public class TagPairBuilder
    {
        private Type converterType;
        private TagToken openToken;
        private TagToken closeToken;

        public TagPairBuilder AddOpenToken(TagToken openToken)
        {
            this.openToken = openToken;
            return this;
        }

        public TagPairBuilder AddCloseToken(TagToken closeToken)
        {
            this.closeToken = closeToken;
            return this;
        }

        public TagPairBuilder AddConverterType(Type converterType)
        {
            this.converterType = converterType;
            return this;
        }

        public TagPair Build()
        {
            return new TagPair {OpenToken = openToken, CloseToken = closeToken, ConverterType = converterType};
        }
    }
}