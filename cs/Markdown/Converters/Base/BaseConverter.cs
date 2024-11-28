using Markdown.Models;

namespace Markdown.Converters.Base;

public abstract class BaseConverter : IConverter
{
    protected abstract string StartOriginalValue { get; }
    protected abstract string EndOriginalValue { get; }
    protected abstract string StartConvertedValue { get; }
    protected abstract string EndConvertedValue { get; }
    
    public virtual IEnumerable<TagPair> Convert(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            yield break;
        }
        var index = 0;
        var isInTag = false;
        var isBetweenWord = false;
        var tagPairBuilder = TagPair.CreateBuilder(GetType());
        while (index < text.Length)
        {
            if (!isInTag && IsTagStart(text, index) && !HasShielding(text, index - 1))
            {
                isBetweenWord = HasBetweenWord(text, index);
                var token = new TagToken(StartOriginalValue, StartConvertedValue, index);
                tagPairBuilder.AddOpenToken(token);
                index += StartOriginalValue.Length;
                isInTag = true;
                continue;
            }

            if (isInTag && IsShouldAbort(text, index, isBetweenWord))
            {
                isBetweenWord = false;
                isInTag = false;
                continue;
            }

            if (isInTag && IsTagEnd(text, index) && !HasShielding(text, index - 1))
            {
                isBetweenWord = false;
                var token = new TagToken(EndOriginalValue, EndConvertedValue, index);
                tagPairBuilder.AddCloseToken(token);
                yield return tagPairBuilder.Build();
                index += EndOriginalValue.Length;
                isInTag = false;
                continue;
            }

            index++;
        }
    }

    protected virtual bool HasShielding(string text, int index)
    {
        if (index < 0 || index >= text.Length)
        {
            return false;
        }

        return text[index].Equals('\\') && (index - 1 < 0 || !text[index - 1].Equals('\\'));
    }

    protected virtual bool HasBetweenWord(string text, int index)
    {
        if (index - 1 < 0 || index + StartOriginalValue.Length >= text.Length)
        {
            return false;
        }

        var hasNextIsWord = char.IsLetter(text, index + StartOriginalValue.Length);
        var hasBeforeIsWord = char.IsLetter(text, index - 1);
        return hasNextIsWord && hasBeforeIsWord;
    }

    protected virtual bool IsShouldAbort(string text, int index, bool isStartBeforeWord = false)
    {
        if (isStartBeforeWord && char.IsWhiteSpace(text, index))
        {
            return true;
        }

        return ShouldAbortTag(text, index);
    }

    protected virtual bool HasEndWithBeforeWhiteSpace(string text, int endIndex)
    {
        if (endIndex - 1 < 0)
        {
            return false;
        }

        var isEndIndex = IsTagEnd(text, endIndex);
        var previousIndex = endIndex - 1;
        return isEndIndex && char.IsWhiteSpace(text, previousIndex);
    }

    protected abstract bool IsTagStart(string text, int index);

    protected abstract bool IsTagEnd(string text, int index);

    protected abstract bool ShouldAbortTag(string text, int index);
}