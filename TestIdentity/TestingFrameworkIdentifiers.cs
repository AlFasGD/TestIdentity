namespace TestIdentity;

#nullable enable

/// <summary>
/// Contains information about a testing framework's common identifiers.
/// </summary>
public abstract class TestingFrameworkIdentifiers
{
    private static readonly CaseInvariantNameDictionary<TestingFramework> testingFrameworkDictionary = new();

    static TestingFrameworkIdentifiers()
    {
        testingFrameworkDictionary.MapStringRepresentations(TestingFramework.NUnit, TestingFramework.XUnit, TestingFramework.MSTest);
    }

    /// <summary>Gets the code-friendly name of the framework. Defaults to <seealso cref="FrameworkName"/>.</summary>
    public virtual string CodeFrameworkName => FrameworkName;

    /// <summary>Gets the official name of the framework.</summary>
    public abstract string FrameworkName { get; }
    /// <summary>Gets the namespace that contains the attributes that are exposed to the user.</summary>
    public abstract string AttributeNamespace { get; }

    /// <summary>Gets the name of the attribute that reflects adding inline data to the test method.</summary>
    public abstract string InlineDataAttributeName { get; }
    /// <summary>Gets the name of the attribute that reflects a parameterized test method.</summary>
    public abstract string ParameterizedTestMethodAttributeName { get; }
    /// <summary>Gets the name of the attribute that denotes a class as a test class.</summary>
    /// <remarks>This is only mandatory in MSTest.</remarks>
    public abstract string? TestClassAttributeName { get; }

    /// <summary>Gets the testing framework whose identifier information is contained in this instance.</summary>
    public abstract TestingFramework TestingFramework { get; }

    public static TestingFrameworkIdentifiers? GetForFrameworkCodeName(string name)
    {
        return GetForFramework(GetTestingFrameworkByCodeName(name));
    }
    public static TestingFramework GetTestingFrameworkByCodeName(string name)
    {
        return testingFrameworkDictionary.GetMappedValue(name);
    }
    public static TestingFrameworkIdentifiers? GetForFramework(TestingFramework framework)
    {
        return framework switch
        {
            TestingFramework.NUnit => NUnitIdentifiers.Instance,
            TestingFramework.XUnit => XUnitIdentifiers.Instance,
            TestingFramework.MSTest => MSTestIdentifiers.Instance,

            _ => null,
        };
    }
}

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

/// <summary>
/// Contains information about the NUnit testing framework's common identifiers.
/// </summary>
public sealed class NUnitIdentifiers : TestingFrameworkIdentifiers
{
    public static readonly NUnitIdentifiers Instance = new();

    private NUnitIdentifiers() { }

    public const string CodeName = nameof(TestingFramework.NUnit);

    public override string FrameworkName => CodeName;
    public override string AttributeNamespace => "NUnit.Framework";

    public override string InlineDataAttributeName => "TestCase";
    public override string ParameterizedTestMethodAttributeName => "Test";
    public override string? TestClassAttributeName => null;

    public override TestingFramework TestingFramework => TestingFramework.NUnit;
}
/// <summary>
/// Contains information about the xUnit testing framework's common identifiers.
/// </summary>
public sealed class XUnitIdentifiers : TestingFrameworkIdentifiers
{
    public static readonly XUnitIdentifiers Instance = new();

    private XUnitIdentifiers() { }

    public const string CodeName = nameof(TestingFramework.XUnit);

    // We have a bit of an issue with casing
    public override string CodeFrameworkName => CodeName;
    public override string FrameworkName => "xUnit";
    public override string AttributeNamespace => "Xunit";

    // Conflicts with NUnit, should not be a real scenario
    public override string InlineDataAttributeName => "InlineData";
    public override string ParameterizedTestMethodAttributeName => "Theory";
    public override string? TestClassAttributeName => null;

    public override TestingFramework TestingFramework => TestingFramework.XUnit;
}
/// <summary>
/// Contains information about the MSTest testing framework's common identifiers.
/// </summary>
public sealed class MSTestIdentifiers : TestingFrameworkIdentifiers
{
    public static readonly MSTestIdentifiers Instance = new();

    private MSTestIdentifiers() { }

    public const string CodeName = nameof(TestingFramework.MSTest);

    public override string FrameworkName => CodeName;
    public override string AttributeNamespace => "Microsoft.VisualStudio.TestTools.UnitTesting";

    public override string InlineDataAttributeName => "DataRow";
    public override string ParameterizedTestMethodAttributeName => "DataTestMethod";
    public override string? TestClassAttributeName => "TestClass";

    public override TestingFramework TestingFramework => TestingFramework.MSTest;
}