using Markdown.Extensions;

namespace Markdown.Converters.Base;

public abstract class BaseHeaderConverter : BaseConverter
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
               && !text.HasAdjacentTag(nextIndex, StartOriginalValue)
               && !text.HasAdjacentTag(previousIndex, StartOriginalValue);
    }

    protected override bool IsTagEnd(string text, int index)
    {
        if (index + 1 == text.Length)
        {
            return true;
        }
        if (index < 0 || index + EndOriginalValue.Length > text.Length)
        {
            return false;
        }
        return text.ContainsTagAtIndex(index, EndOriginalValue);
    }

    protected override bool ShouldAbortTag(string text, int index)
    {
        return !text.IsInBounds(index, EndOriginalValue.Length);
    }
}