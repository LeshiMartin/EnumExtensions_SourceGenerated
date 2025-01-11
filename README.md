# A Repository for generating Extension for enums. 

It has couple of attributes :
- EnumExtension (To specify on which enum should generate extensions)
- EnumName (To specify a differenet name on the EnumMember)
- EnumDescription (To specify a special description)

Just by adding the **EnumExtension** attribute it will generate extension methods for that enum:
- GetName
- GetDescription

If that enum also has a **Flags** attribute it will generate for that enum:
- AddFlag
- RemoveFlag
- ContainsFlag

And also it will generate a static helper class with the same name of the Enum followed by helper.
it will contain:
- GetValues (returning a collection of all of the enum values)
- GetNames(bool respectAttribute)  returning a collection of the names
- TryTryConvertFromName trying to convert the name to the enum with respectAttribute flag
- TryConvertFrom  trying to convert the value to the enum
- GetMaxValue returning the max value
- GetMinValue returning the min value
