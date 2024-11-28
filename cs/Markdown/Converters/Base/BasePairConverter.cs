using Markdown.Extensions;

namespace Markdown.Converters.Base;

public abstract class BasePairConverter : BaseConverter
{
    protected override bool IsTagStart(string text, int index)
    {
        if (!text.IsInBounds(index, StartOriginalValue.Length))
        {
            return false;
        }

        var nextIndex = index + StartOriginalValue.Length;
        var previousIndex = index - StartOriginalValue.Length;
        return text.ContainsTagAtIndex(index, StartOriginalValue)
               && !text.HasFollowingSpace(nextIndex)
               && !text.HasAdjacentTag(nextIndex, StartOriginalValue)
               && !text.HasAdjacentTag(previousIndex, StartOriginalValue);
    }

    protected override bool IsTagEnd(string text, int index)
    {
        if (!text.IsInBounds(index, EndOriginalValue.Length))
        {
            return false;
        }

        var nextIndex = index + EndOriginalValue.Length;
        var previousIndex = index - EndOriginalValue.Length;
        return text.ContainsTagAtIndex(index, EndOriginalValue)
               && !text.HasAdjacentTag(nextIndex, EndOriginalValue)
               && !text.HasAdjacentTag(previousIndex, EndOriginalValue);
    }

    protected override bool ShouldAbortTag(string text, int index)
    {
        if (!text.IsInBounds(index, EndOriginalValue.Length))
        {
            return false;
        }

        var hasDigit = char.IsDigit(text[index]);
        var hasWhiteSpace = HasEndWithBeforeWhiteSpace(text, index);
        return hasDigit || hasWhiteSpace;
    }
}