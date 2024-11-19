namespace Markdown.Models;

public record ConvertToken
{
    private readonly int startPosition;
    private readonly int endPosition;

    private int StartPosition
    {
        get => startPosition;
        init
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "StartPosition cannot be negative.");
            }

            if (value > EndPosition)
            {
                throw new ArgumentException("StartPosition cannot be greater than EndPosition.");
            }

            startPosition = value;
        }
    }

    private int EndPosition
    {
        get => endPosition;
        init
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "EndPosition cannot be negative.");
            }

            if (value < StartPosition)
            {
                throw new ArgumentException("EndPosition cannot be less than StartPosition.");
            }

            endPosition = value;
        }
    }

    public string Content { get; init; }

    public bool WithConvert { get; init; }
    
    public ConvertToken(int startPosition, int endPosition, string content, bool withConvert)
    {
        StartPosition = startPosition;
        EndPosition = endPosition;
        Content = content;
        WithConvert = withConvert;
    }
}