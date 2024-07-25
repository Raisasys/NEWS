using System.Text.RegularExpressions;

namespace Core.Types;

public readonly struct Password : IEquatable<Password>
{
    internal Password(string value)
    {
        this.value = value;
    }

    internal string value { get; }
    public override int GetHashCode() => value.GetHashCode();
    public bool Equals(Password other) => value == other.value;

    public int Size() => value.Length;
    public override string ToString() => value;

    public static implicit operator Password(string key) => new Password(key);

    public static implicit operator string(Password key) => key.value;
}


public static class PasswordUtils
{
    static readonly Regex LowercaseRegex = new Regex("[a-z]+");
    public static bool HasLowercase(this Password password) => LowercaseRegex.IsMatch(password);


    static readonly Regex UppercaseRegex = new Regex("[A-Z]+");
    public static bool HasUppercase(this Password password) => UppercaseRegex.IsMatch(password);


    static readonly Regex DigitRegex = new Regex("(\\d)+");
    public static bool HasDigit(this Password password) => DigitRegex.IsMatch(password);


    static readonly Regex SymbolRegex = new Regex("(\\W)+");
    public static bool HasSymbol(this Password password) => SymbolRegex.IsMatch(password);


    private static readonly int MinLength = 8;
    public static bool HasMinLength(this Password password) => password.Size() >= MinLength;

}
