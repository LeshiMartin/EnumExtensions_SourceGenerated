using System;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EnumExtensionsGenerator;

internal static class Extensions
{

    public static AttributeSyntax GetAttributeSyntax(this EnumDeclarationSyntax node, string attributeName)
        => node.GetAttributes().First(x => x.Name.ToString().Equals(attributeName, StringComparison.OrdinalIgnoreCase));

    public static AttributeSyntax GetAttributeSyntax(this EnumMemberDeclarationSyntax node, string attributeName)
          => node.GetAttributes().First(x => x.Name.ToString().Equals(attributeName, StringComparison.OrdinalIgnoreCase));
    public static bool HasAttribute(this EnumDeclarationSyntax enumDeclarationSyntax, string attributeName)
    => enumDeclarationSyntax.GetAttributes().Any(x => x.Name.ToString().Equals(attributeName, StringComparison.OrdinalIgnoreCase));

    public static bool HasAttribute(this EnumMemberDeclarationSyntax enumDeclarationSyntax, string attributeName)
   => enumDeclarationSyntax.GetAttributes().Any(x => x.Name.ToString().Equals(attributeName, StringComparison.OrdinalIgnoreCase));

    public static string GetNamespaceName(this SyntaxNode syntaxNode, string fallBackValue = "")
        => syntaxNode switch
        {
            FileScopedNamespaceDeclarationSyntax fileScopedNamespace => fileScopedNamespace.Name.ToString(),
            NamespaceDeclarationSyntax namespaceDeclaration => namespaceDeclaration.Name.ToString(),
            _ => fallBackValue
        };


    public static string GetNameMethod(this EnumDeclarationSyntax @enum)
    {
        var enumName = @enum.Identifier.ToString();

        var docSb = new StringBuilder()
            .AppendLine("/// <summary>")
            .AppendLine("/// Gets the name of the enum <paramref name=\"source\" /> for:")
            .AppendLine("/// <c>");
        var methodSb = new StringBuilder();
        methodSb.AppendLine(
            $$"""
                  public static string GetName(this {{enumName}} source)
                   => source switch 
                    {
              """);

        foreach (var member in @enum.Members)
        {
            var memberName = member.Identifier.ToString();
            var stringRep = member.GetDesiredMemberName(memberName);
            docSb.AppendLine($"/// <br /> <see cref=\"{enumName}.{memberName}\"/>  => {stringRep} ;");
            methodSb.AppendLine($"       {enumName}.{memberName} => \"{stringRep}\",");
        }

        methodSb
            .AppendLine("        _ => throw new ArgumentOutOfRangeException()")
            .AppendLine("     };");
        docSb
        .AppendLine("///</c>")
        .AppendLine("///</summary>")
            .AppendLine("///<param name=\"source\">The enum from which the name will be retrieved</param>")
            .AppendLine(
                "/// <exception cref=\"ArgumentOutOfRangeException\"> if <paramref name=\"source\" /> cannot be matched </exception>")
            .AppendLine("///<returns> The string representation of <paramref name=\"source\"/> </returns>");

        return docSb.AppendLine(methodSb.ToString()).ToString();
    }

    public static string GetDescriptionMethod(this EnumDeclarationSyntax @enum)
    {
        var enumName = @enum.Identifier.ToString();

        var docSb = new StringBuilder()
            .AppendLine("/// <summary>")
            .AppendLine("/// Gets the description of the enum <paramref name=\"source\" /> for:")
            .AppendLine("/// <c>");
        var methodSb = new StringBuilder();
        methodSb.AppendLine(
            $$"""
                  public static string GetDescription(this {{enumName}} source)
                   => source switch 
                    {
              """);

        foreach (var member in @enum.Members)
        {
            var memberName = member.Identifier.ToString();
            var stringRep = member.GetDesiredMemberDescription(memberName);
            docSb.AppendLine($"/// <br /> <see cref=\"{enumName}.{memberName}\"/> => {stringRep} ;");
            methodSb.AppendLine($"       {enumName}.{memberName} => \"{stringRep}\",");
        }

        methodSb
            .AppendLine("        _ => throw new ArgumentOutOfRangeException()")
            .AppendLine("     };");
        docSb
            .AppendLine("///</c>")
            .AppendLine("///</summary>")
            .AppendLine("///<param name=\"source\">The enum from which the description will be retrieved</param>")
            .AppendLine(
                "/// <exception cref=\"ArgumentOutOfRangeException\"> if <paramref name=\"source\" /> cannot be matched </exception>")
            .AppendLine("///<returns> The description of <paramref name=\"source\"/> </returns>");

        return docSb.AppendLine(methodSb.ToString()).ToString();
    }

    private static IEnumerable<AttributeSyntax> GetAttributes(this EnumDeclarationSyntax enumDeclarationSyntax)
        => enumDeclarationSyntax.AttributeLists.SelectMany(x => x.Attributes);

    private static IEnumerable<AttributeSyntax> GetAttributes(this EnumMemberDeclarationSyntax enumDeclarationSyntax)
   => enumDeclarationSyntax.AttributeLists.SelectMany(x => x.Attributes);

    private static string GetDesiredMemberDescription(this EnumMemberDeclarationSyntax member, string memberName)
    {
        if (!member.HasAttribute(nameof(EnumDescription)))
            return memberName;
        var attributeSyntax = member.GetAttributeSyntax(nameof(EnumDescription));
        var argument = attributeSyntax?.ArgumentList?.Arguments.FirstOrDefault();
        if (argument?.Expression is LiteralExpressionSyntax literalExpressionSyntax)
            return literalExpressionSyntax.Token.ValueText ?? memberName;
        return memberName;
    }

    private static string GetDesiredMemberName(this EnumMemberDeclarationSyntax member, string memberName)
    {
        if (!member.HasAttribute(nameof(EnumName)))
            return memberName;
        var attributeSyntax = member.GetAttributeSyntax(nameof(EnumName));
        var argument = attributeSyntax?.ArgumentList?.Arguments.FirstOrDefault();
        if (argument?.Expression is LiteralExpressionSyntax literalExpressionSyntax)
            return literalExpressionSyntax.Token.ValueText ?? memberName;
        return memberName;
    }


}
