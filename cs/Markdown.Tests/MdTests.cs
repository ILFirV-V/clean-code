using FluentAssertions;
using Markdown.Models;

namespace Markdown.Tests;

[TestFixture]
[TestOf(typeof(Md))]
public partial class MdTests
{
    private Md md = new();
    
    [Test]
    [TestCaseSource(nameof(conversionTestCases))]
    [TestCaseSource(nameof(noConversionTestCases))]
    public void Convert_ShouldCorrectlyConvert_VariousInputs(string text, string expected)
    {
        var convertResult = md.Render(text);
        convertResult.Should().BeEquivalentTo(expected);
    }
}
