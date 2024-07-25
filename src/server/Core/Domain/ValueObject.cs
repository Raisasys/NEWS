using System.Reflection;


namespace Core;


[Serializable]
public abstract class ValueObject : IComparable, IComparable<ValueObject>, IEquatable<ValueObject>
{
    private int? _cachedHashCode;

    private List<PropertyInfo> _properties;

    private List<FieldInfo> _fields;

    protected abstract IEnumerable<object> GetEqualityComponents();

    public bool Equals(ValueObject obj) => Equals(obj as object);

    public override bool Equals(object obj)
    {
        if (obj is null)
            return false;

        if (GetUnproxiedType(this) != GetUnproxiedType(obj))
            return false;

        var valueObject = (ValueObject)obj;

        return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        if(_cachedHashCode.HasValue)
            return _cachedHashCode.Value;

        unchecked
        {
            int hash = 17;
            foreach (var prop in GetProperties())
            {
                var value = prop.GetValue(this, null);
                hash = HashValue(hash, value);
            }

            foreach (var field in GetFields())
            {
                var value = field.GetValue(this);
                hash = HashValue(hash, value);
            }

            _cachedHashCode = hash;
            return hash;
        }
    }

    public int CompareTo(object obj)
    {
        if (obj == null)
            return 1;

        var thisType = GetUnproxiedType(this);
        var otherType = GetUnproxiedType(obj);

        if (thisType != otherType)
            return string.Compare(thisType.ToString(), otherType.ToString(), StringComparison.Ordinal);

        var other = (ValueObject)obj;

        var components = GetEqualityComponents().ToArray();
        var otherComponents = other.GetEqualityComponents().ToArray();

        for (var i = 0; i < components.Length; i++)
        {
            var comparison = CompareComponents(components[i], otherComponents[i]);
            if (comparison != 0)
                return comparison;
        }

        return 0;
    }

    private static int CompareComponents(object object1, object object2)
    {
        if (object1 is null && object2 is null)
            return 0;

        if (object1 is null)
            return -1;

        if (object2 is null)
            return 1;

        if (object1 is IComparable comparable1 && object2 is IComparable comparable2)
            return comparable1.CompareTo(comparable2);

        return object1.Equals(object2) ? 0 : -1;
    }

    public int CompareTo(ValueObject other)
    {
        return CompareTo(other as object);
    }

    public static bool operator ==(ValueObject a, ValueObject b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(ValueObject a, ValueObject b)
    {
        return !(a == b);
    }

    internal static Type GetUnproxiedType(object obj)
    {
#if NET8_0
        ArgumentNullException.ThrowIfNull(obj);
#endif

#if NETSTANDARD2_0_OR_GREATER
        if(obj is null)
            throw new ArgumentNullException(nameof(obj));
#endif

        const string efCoreProxyPrefix = "Castle.Proxies.";
        const string nHibernateProxyPostfix = "Proxy";

        var type = obj.GetType();
        var typeString = type.ToString();

        if (typeString.Contains(efCoreProxyPrefix) || typeString.EndsWith(nHibernateProxyPostfix))
            return type.BaseType!;

        return type;
    }

    protected static void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
        {
            throw new BusinessRuleValidationException(rule);
        }
    }

    private bool PropertiesAreEqual(object obj, PropertyInfo p)
    {
        return object.Equals(p.GetValue(this, null), p.GetValue(obj, null));
    }

    private bool FieldsAreEqual(object obj, FieldInfo f)
    {
        return object.Equals(f.GetValue(this), f.GetValue(obj));
    }

    private IEnumerable<PropertyInfo> GetProperties()
    {
        if (_properties != null) return _properties;

        _properties = GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => p.GetCustomAttribute(typeof(IgnoreMemberAttribute)) == null)
            .ToList();
        // Not available in Core
        // !Attribute.IsDefined(p, typeof(IgnoreMemberAttribute))).ToList();
        return this._properties;
    }

    private IEnumerable<FieldInfo> GetFields()
    {
        if (_fields != null) return _fields;

        _fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(p => p.GetCustomAttribute(typeof(IgnoreMemberAttribute)) == null)
            .ToList();
        return _fields;
    }
    
    private static int HashValue(int seed, object value)
    {
        var currentHash = value?.GetHashCode() ?? 0;
        return (seed * 23) + currentHash;
    }
}