using System.Text.RegularExpressions;

namespace Core.Types;

public readonly struct PhoneNumber : IEquatable<PhoneNumber>
{
    internal PhoneNumber(string value)
    {
        this.value = value;
    }

    internal string value { get; }
    public override int GetHashCode() => value.GetHashCode();
    public bool Equals(PhoneNumber other) => value == other.value;

    public int Size() => value.Length;
    public override string ToString() => value;

    public static implicit operator PhoneNumber(string key) => new PhoneNumber(key);

    public static implicit operator string(PhoneNumber key) => key.value;
}


public static class PhoneNumberUtils
{
    static readonly Regex PhoneNumberRegex = new Regex(@"^(?:\+?(61))? ?(?:\((?=.*\)))?(0?[2-57-8])\)? ?(\d\d(?:[- ](?=\d{3})|(?!\d\d[- ]?\d[- ]))\d\d[- ]?\d[- ]?\d{3})$");
    public static bool HasPhoneNumber(this string number) => PhoneNumberRegex.IsMatch(number);
}
