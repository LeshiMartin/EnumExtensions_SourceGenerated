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

    [Fact]
    public void GetValue_Tests()
    {
        var value = SimpleEnumHelper.GetMaxValue();
        value.Should().Be((int)SimpleEnum.Four);
    }

    [Fact]
    public void GetNames_Tests()
    {
        var names = SimpleEnumHelper.GetNames();
        names.Should().NotBeEmpty();
    }

    [Theory]
    [InlineData(FlagEnum.None, FlagEnum.None, true)]
    [InlineData(FlagEnum.None, FlagEnum.First, false)]
    [InlineData(FlagEnum.None, FlagEnum.Second, false)]
    [InlineData(FlagEnum.None, FlagEnum.Third, false)]
    [InlineData(FlagEnum.None, FlagEnum.Fourth, false)]
    [InlineData(FlagEnum.None, FlagEnum.OneAndTwo, false)]
    [InlineData(FlagEnum.None, FlagEnum.TwoAndThree, false)]
    [InlineData(FlagEnum.None, FlagEnum.All, false)]
    [InlineData(FlagEnum.First, FlagEnum.First, true)]
    [InlineData(FlagEnum.First, FlagEnum.Second, false)]
    [InlineData(FlagEnum.First, FlagEnum.Third, false)]
    [InlineData(FlagEnum.First, FlagEnum.Fourth, false)]
    [InlineData(FlagEnum.First, FlagEnum.OneAndTwo, false)]
    [InlineData(FlagEnum.First, FlagEnum.TwoAndThree, false)]
    [InlineData(FlagEnum.First, FlagEnum.All, false)]
    [InlineData(FlagEnum.Second, FlagEnum.Second, true)]
    [InlineData(FlagEnum.Second, FlagEnum.Third, false)]
    [InlineData(FlagEnum.Second, FlagEnum.Fourth, false)]
    [InlineData(FlagEnum.Second, FlagEnum.OneAndTwo, false)]
    [InlineData(FlagEnum.Second, FlagEnum.TwoAndThree, false)]
    [InlineData(FlagEnum.Second, FlagEnum.All, false)]
    [InlineData(FlagEnum.Third, FlagEnum.Third, true)]
    [InlineData(FlagEnum.Third, FlagEnum.Fourth, false)]
    [InlineData(FlagEnum.Third, FlagEnum.OneAndTwo, false)]
    [InlineData(FlagEnum.Third, FlagEnum.TwoAndThree, false)]
    [InlineData(FlagEnum.Third, FlagEnum.All, false)]
    [InlineData(FlagEnum.Fourth, FlagEnum.Fourth, true)]
    [InlineData(FlagEnum.Fourth, FlagEnum.OneAndTwo, false)]
    [InlineData(FlagEnum.Fourth, FlagEnum.TwoAndThree, false)]
    [InlineData(FlagEnum.Fourth, FlagEnum.All, false)]
    [InlineData(FlagEnum.OneAndTwo, FlagEnum.OneAndTwo, true)]
    [InlineData(FlagEnum.OneAndTwo, FlagEnum.None, true)]
    [InlineData(FlagEnum.OneAndTwo, FlagEnum.First, true)]
    [InlineData(FlagEnum.OneAndTwo, FlagEnum.Second, true)]
    [InlineData(FlagEnum.OneAndTwo, FlagEnum.Third, false)]
    [InlineData(FlagEnum.OneAndTwo, FlagEnum.Fourth, false)]
    [InlineData(FlagEnum.OneAndTwo, FlagEnum.TwoAndThree, false)]
    [InlineData(FlagEnum.OneAndTwo, FlagEnum.All, false)]
    [InlineData(FlagEnum.TwoAndThree, FlagEnum.OneAndTwo, false)]
    [InlineData(FlagEnum.TwoAndThree, FlagEnum.None, true)]
    [InlineData(FlagEnum.TwoAndThree, FlagEnum.First, false)]
    [InlineData(FlagEnum.TwoAndThree, FlagEnum.Second, true)]
    [InlineData(FlagEnum.TwoAndThree, FlagEnum.Third, true)]
    [InlineData(FlagEnum.TwoAndThree, FlagEnum.Fourth, false)]
    [InlineData(FlagEnum.TwoAndThree, FlagEnum.TwoAndThree, true)]
    [InlineData(FlagEnum.TwoAndThree, FlagEnum.All, false)]
    [InlineData(FlagEnum.TwoAndFour, FlagEnum.OneAndTwo, false)]
    [InlineData(FlagEnum.TwoAndFour, FlagEnum.None, true)]
    [InlineData(FlagEnum.TwoAndFour, FlagEnum.First, false)]
    [InlineData(FlagEnum.TwoAndFour, FlagEnum.Second, true)]
    [InlineData(FlagEnum.TwoAndFour, FlagEnum.Third, false)]
    [InlineData(FlagEnum.TwoAndFour, FlagEnum.Fourth, true)]
    [InlineData(FlagEnum.TwoAndFour, FlagEnum.TwoAndFour, true)]
    [InlineData(FlagEnum.TwoAndFour, FlagEnum.All, false)]
    [InlineData(FlagEnum.All, FlagEnum.OneAndTwo, true)]
    [InlineData(FlagEnum.All, FlagEnum.None, true)]
    [InlineData(FlagEnum.All, FlagEnum.First, true)]
    [InlineData(FlagEnum.All, FlagEnum.Second, true)]
    [InlineData(FlagEnum.All, FlagEnum.Third, true)]
    [InlineData(FlagEnum.All, FlagEnum.Fourth, true)]
    [InlineData(FlagEnum.All, FlagEnum.TwoAndFour, true)]
    [InlineData(FlagEnum.All, FlagEnum.All, true)]
    public void ContainsFlagTest(FlagEnum testSubject, FlagEnum testAgainst, bool expectedResult) =>
        testSubject.ContainsFlag(testAgainst).Should().Be(expectedResult);


    [Fact]
    public void Add_Flag_Should_Add_The_Flag_ToThe_Enum()
    {
        var first = FlagEnum.First;
        var withFlag1= first.AddFlag(FlagEnum.Second);
        withFlag1.ContainsFlag(FlagEnum.Second).Should().BeTrue();
        var withFlag2 = withFlag1.AddFlag(FlagEnum.Third);
        withFlag2.ContainsFlag(FlagEnum.Third).Should().BeTrue();
    }

    [Fact]
    public void Remove_Flag_Should_Remove_The_Flag_ToThe_Enum()
    {
        var first = FlagEnum.First;
        var withFlag1= first.AddFlag(FlagEnum.Second);
        withFlag1.ContainsFlag(FlagEnum.Second).Should().BeTrue();
        var withFlag2 = withFlag1.AddFlag(FlagEnum.Third);
        withFlag2.ContainsFlag(FlagEnum.Third).Should().BeTrue();

        var removed = withFlag2.RemoveFlag(FlagEnum.Third);
        removed.ContainsFlag(FlagEnum.Third).Should().BeFalse();
        var removed2 = removed.RemoveFlag(FlagEnum.Second);
        removed2.ContainsFlag(FlagEnum.Second).Should().BeFalse();
    }
}