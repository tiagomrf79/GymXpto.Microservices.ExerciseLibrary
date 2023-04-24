// Enums are intended for cases when you have enumerated every possible value (for example days of the week or months of the year).
// Elsewhere it leads to fragile code with many control flow statements to check its value.
// Adding a new value would mean changing the code everywhere where control flow is used (against Open/Closed principle).

// TODO: Check if the Enumeration class needs to be public
// TODO: Confirm what is the return of the method GetAll, of the Enumeration class

using System.Reflection;

namespace Domain.SeedWork;

public abstract class Enumeration : IComparable
{
    //INSTANCE PROPERTIES AND METHODS

    public string Name { get; }
    public int Id { get; }

    protected Enumeration(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public override string ToString()
    {
        return Name;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Enumeration otherValue)
            return false;

        bool typeMatches = GetType().Equals(obj.GetType());
        bool valueMatches = Id.Equals(otherValue.Id);

        return typeMatches && valueMatches;
    }

    // implements IComparable so it can sort using the Id
    public int CompareTo(object? other)
    {
        if (other == null) return 1;

        Enumeration? otherEnumeration = other as Enumeration;
        if (otherEnumeration != null)
            return Id.CompareTo(otherEnumeration.Id);
        else
            throw new ArgumentException("Object is not a Enumeration");
    }

    // STATIC METHODS

    // get a list of fields for a type, using reflection
    public static IEnumerable<T> GetAll<T>() where T : Enumeration
    {
        return typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                        .Select(f => f.GetValue(null))
                        .Cast<T>();
    }

    // get a instance of type using Id
    public static T FromValue<T>(int value) where T : Enumeration
    {
        var matchingItem = Parse<T, int>(value, "value", item => item.Id == value);
        return matchingItem;
    }

    // get a instance of type using name
    public static T FromDisplayName<T>(string displayName) where T : Enumeration
    {
        var matchingItem = Parse<T, string>(displayName, "display name", item => item.Name == displayName);
        return matchingItem;
    }

    // find an instance using its type, property and property value
    private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration
    {
        var matchingItem = GetAll<T>().FirstOrDefault(predicate);

        if (matchingItem == null)
            throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

        return matchingItem;
    }

}