using EnumExtensionsTest.Enums;
using FluentAssertions;

namespace EnumExtensionsTest;

public class Tests
{
    [Theory]
    [InlineData(SimpleEnum.One, "One")]
    [InlineData(SimpleEnum.Two, "Two")]
    [InlineData(SimpleEnum.Three, "Three")]
    [InlineData(SimpleEnum.Four, "Cetiri")]
    [InlineData(SimpleEnum.None, "None")]
    public void GetName_Tests(SimpleEnum @enum, string expectedName)
    => @enum.GetName().Should().Be(expectedName);

    [Theory]
    [InlineData(SimpleEnum.One, "One")]
    [InlineData(SimpleEnum.Two, "Two")]
    [InlineData(SimpleEnum.Three, "Three")]
    [InlineData(SimpleEnum.Four, "SimpleDescription")]
    [InlineData(SimpleEnum.None, "None")]
    public void GetDescription_Tests(SimpleEnum @enum, string expectedDescription)
        => @enum.GetDescription().Should().Be(expectedDescription);

}