using EnumExtensionsGenerator;

namespace EnumExtensionsTest.Enums;

[EnumExtensions]
public enum SimpleEnum
{
    None,
    One,
    Two,
    Three,

    [EnumName("Cetiri"), EnumDescription("SimpleDescription")]
    Four
}

[EnumExtensions]
public enum SimpleByteEnum : byte
{
    None,
    One,
    Two,
    Three,

    [EnumName("Cetiri"), EnumDescription("SimpleDescription")]
    Four,
}

[EnumExtensions, Flags]
public enum FlagEnum
{
    None,
    First,
    Second,
    Third = 4,
    Fourth = 8,
    TwoAndThree = Second | Third,
    OneAndTwo = First | Second,
    TwoAndFour = Second | Fourth,
    All = First | Second | Third | Fourth,
}