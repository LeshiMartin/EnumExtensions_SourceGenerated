using EnumExtensionsGenerator;

namespace EnumExtensionsTest.Enums;

[EnumExtensions]
public enum SimpleEnum
{
    None,
    One,
    Two,
    Three,
    [EnumName("Cetiri")]
    [EnumDescription("SimpleDescription")]
    Four,
}
