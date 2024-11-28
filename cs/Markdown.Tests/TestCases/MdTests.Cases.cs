namespace Markdown.Tests;

public partial class MdTests
{
    private static readonly IEnumerable<TestCaseData> conversionTestCases =
    [
        new TestCaseData(
                "Внутри __двойного выделения _одинарное_ тоже__ работает.",
                "Внутри <b>двойного выделения <i>одинарное</i> тоже</b> работает.")
            .SetName("NestedTags_ItalicInBold"),
        new TestCaseData(
                "Но не наоборот — внутри _одинарного __двойное__ не_ работает.",
                "Но не наоборот — внутри <i>одинарного __двойное__ не</i> работает.")
            .SetName("NestedTags_BoldInItalic_Fails"),
        new TestCaseData(
                "# Заголовок __с _разными_ символами__",
                "<h1>Заголовок <b>с <i>разными</i> символами</b></h1>")
            .SetName("OverlappingTags"),
    ];
    
    private static readonly IEnumerable<TestCaseData> noConversionTestCases =
    [
        new TestCaseData(
                "В случае __пересечения _двойных__ и одинарных_ подчерков ни один из них не считается выделением.",
                "В случае __пересечения _двойных__ и одинарных_ подчерков ни один из них не считается выделением.")
            .SetName("OverlappingTags"),
    ];
}