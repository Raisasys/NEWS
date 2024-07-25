namespace Core;

public readonly struct ExceptionMessage : IEquatable<ExceptionMessage>
{
    internal ExceptionMessage(string keyValue)
    {
        KeyValue = keyValue;
    }

    internal string KeyValue { get; }
    public override int GetHashCode() => KeyValue.GetHashCode();
    public bool Equals(ExceptionMessage other) => KeyValue == other.KeyValue;

    public override string ToString() => KeyValue;

    public static implicit operator ExceptionMessage(string key) => new ExceptionMessage(key);

    public static implicit operator string(ExceptionMessage key) => key.KeyValue;
}