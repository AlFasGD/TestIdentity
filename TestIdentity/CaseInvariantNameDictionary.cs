using System;
using System.Collections.Generic;

namespace TestIdentity;

/// <summary>Provides a case-invariant name dictionary, mapping names to enum values.</summary>
/// <typeparam name="T">The type of values mapped to the names.</typeparam>
/// <remarks>
/// Use the <seealso cref="Map(string, T)"/> method for mapping a name, which converts the entire string to lower characters, enabling correct matching.
/// For already lowered strings, and to slightly improve performance, <seealso cref="Dictionary{string, T}.Add(string, T)"/> will do.
/// </remarks>
public class CaseInvariantNameDictionary<T> : Dictionary<string, T>
    where T : struct, Enum
{
    /// <summary>Gets the default value that reflects an unmapped name.</summary>
    public virtual T DefaultValue { get; } = default;

    /// <summary>Maps the lowercase version of the specified name to the specified value.</summary>
    /// <param name="name">The name to map to the value.</param>
    /// <param name="value">The value to be mapped.</param>
    public void Map(string name, T value)
    {
        Add(name.ToLower(), value);
    }

    /// <summary>Maps the lowercase version of the specified range of names to the specified value.</summary>
    /// <param name="value">The value to map the names to.</param>
    /// <param name="names">The names whose lowercase versions to map to the value.</param>
    public void MapRange(T value, params string[] names)
    {
        foreach (var name in names)
            Map(name, value);
    }

    /// <summary>Maps the given value to its string representation.</summary>
    /// <param name="value">The value whose string representation is being mapped to.</param>
    public void MapStringRepresentation(T value)
    {
        Map(value.ToString(), value);
    }
    /// <summary>Maps the given values to their string representation.</summary>
    /// <param name="values">The values whose string representations are being mapped to.</param>
    public void MapStringRepresentations(params T[] values)
    {
        foreach (var value in values)
            MapStringRepresentation(value);
    }

    /// <summary>Gets the mapped value for the given name.</summary>
    /// <param name="name">The name whose mapped value to get.</param>
    /// <returns>The value that the given name is mapped to, if found, otherwise <seealso cref="DefaultValue"/>.</returns>
    /// <remarks>The provided name is converted to lowercase and then compared against the stored names.</remarks>
    public T GetMappedValue(string name)
    {
        if (TryGetValue(name.ToLower(), out var language))
            return language;

        return DefaultValue;
    }
}
