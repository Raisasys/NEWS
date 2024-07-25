﻿using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;
using Humanizer;

namespace Core;

public static class CoreExtensions
{
    #region String

    public static string ToBase64(this string text) 
	    => System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(text));

    
    public static bool EndsWithAny(this string @this, params string[] listOfEndings)
    {
        foreach (var option in listOfEndings)
            if (@this.EndsWith(option)) return true;

        return false;
    }
    
    public static string Pluralize(this string name)
        => name.Pluralize(inputIsKnownToBeSingular: true);
    public static string PluralizeTypeName(this Type type)
        => type.Name.Pluralize(inputIsKnownToBeSingular: true);
    public static string PluralizeTypeName<T>()
        => typeof(T).Name.Pluralize(inputIsKnownToBeSingular: true);
    
    

    public delegate bool TryParseProvider(string text, Type type, out object result);
    public static readonly List<TryParseProvider> TryParseProviders = new();

    static string RemoveFromOrAfter(this string @this, string phrase, bool trimPhrase, bool caseSensitive)
    {
        if (@this.IsEmpty()) return @this;

        int index;

        if (caseSensitive) index = @this.IndexOf(phrase);
        else index = @this.IndexOf(phrase, StringComparison.OrdinalIgnoreCase);

        if (index == -1) return @this;

        if (!trimPhrase) index += phrase.Length;

        return @this.Substring(0, index);
    }
    static string RemoveStart(this string @this, string search, bool trimPhrase = false, bool caseSensitive = false)
    {
        if (@this.IsEmpty()) return @this;

        int index;

        if (caseSensitive) index = @this.IndexOf(search);
        else
            index = @this.IndexOf(search, StringComparison.OrdinalIgnoreCase);

        if (index == -1) return @this;

        @this = @this.Substring(index);

        if (trimPhrase) @this = @this.TrimStart(search, caseSensitive);

        return @this;
    }
    static bool TryParseToCommonTypes<T>(this string text, out T? result) where T : struct
    {
        result = null;

        if (typeof(T) == typeof(int))
        {
            if (int.TryParse(text, out var tempResult)) result = (T)(object)tempResult;

            return true;
        }

        if (typeof(T) == typeof(double))
        {
            if (double.TryParse(text, out var tempResult)) result = (T)(object)tempResult;

            return true;
        }

        if (typeof(T) == typeof(decimal))
        {
            if (decimal.TryParse(text, out var tempResult)) result = (T)(object)tempResult;

            return true;
        }

        if (typeof(T) == typeof(bool))
        {
            if (bool.TryParse(text, out var tempResult)) result = (T)(object)tempResult;

            return true;
        }

        if (typeof(T) == typeof(DateTime))
        {
            if (DateTime.TryParse(text, out var tempResult)) result = (T)(object)tempResult;

            return true;
        }

        if (typeof(T) == typeof(TimeSpan))
        {
            if (TimeSpan.TryParse(text, out var tempResult)) result = (T)(object)tempResult;

            return true;
        }

        if (typeof(T) == typeof(Guid))
        {
            if (Guid.TryParse(text, out var tempResult)) result = (T)(object)tempResult;

            return true;
        }

        if (typeof(T).IsEnum)
        {
            if (Enum.TryParse<T>(text, ignoreCase: true, result: out var tempResult)) result = (T)(object)tempResult;

            return true;
        }

        return false;
    }
    #endregion


    

    #region Reflection
    public static bool IsCalculated(this PropertyInfo property)
        => property.IsDefined(typeof(CalculatedAttribute), inherit: true);
    public static bool IsPrimaryKey(this PropertyInfo property, Type ownerType)
    {
        return property.IsDefined(typeof(PrimaryKeyAttribute), inherit: false) && property.DeclaringType == ownerType;
    }
    public static bool IsIdentifierKey(this PropertyInfo property, Type ownerType)
    {
        return property.IsDefined(typeof(IdentifierKeyAttribute), inherit: false) && property.DeclaringType == ownerType;
    }
    public static bool IsUnique(this PropertyInfo property)
    {
        return property.IsDefined(typeof(IsUniqueAttribute) ,inherit: false);
    }
    public static bool IsImmutable(this PropertyInfo property)
    {
        return property.IsDefined(typeof(ImmutableAttribute), inherit: false);
    }
    public static bool IsAutoGeneratedKey(this Type type, bool inherit = true)
    {
        if (type == null) throw new ArgumentNullException(nameof(type));
        return type.IsDefined(typeof(AutoGeneratedKeyAttribute) ,inherit);
    }
    public static bool IsDateOnly(this PropertyInfo property)
    {
        return property.IsDefined(typeof(DateOnlyAttribute), inherit: false);
    }
    
    static object ChangeType(string text, Type targetType)
    {
        if (targetType == typeof(string)) return text;

        if (text.IsEmpty()) return targetType.GetDefaultValue();

        // Check common types first, for performance:
        if (TryParseToCommonTypes(text, targetType, out var result))
            return result;

        if (targetType.IsEnum) return Enum.Parse(targetType, text);

        if (targetType == typeof(XElement)) return XElement.Parse(text);

        if (targetType == typeof(XDocument)) return XDocument.Parse(text);

        foreach (var parser in TryParseProviders)
            if (parser(text, targetType, out var result2))
                return result2;

        if (targetType.IsNullable())
            return ChangeType(text, targetType.GetGenericArguments().Single());
        else
            return Convert.ChangeType(text, targetType);
    }
    static bool TryParseToCommonTypes(string text, Type targetType, out object result)
    {
        var actualTargetType = targetType;

        var isNullable = targetType.IsNullable();

        if (isNullable)
            targetType = targetType.GetGenericArguments().Single();

        result = null;

        try
        {
            if (targetType == typeof(int)) result = int.Parse(text);

            if (targetType == typeof(long)) result = long.Parse(text);

            if (targetType == typeof(double)) result = double.Parse(text);

            if (targetType == typeof(decimal)) result = decimal.Parse(text);

            if (targetType == typeof(bool)) result = bool.Parse(text);

            if (targetType == typeof(DateTime)) result = DateTime.Parse(text);

            if (targetType == typeof(Guid)) result = new Guid(text);

            if (targetType == typeof(TimeSpan))
            {
                if (text.Is<long>()) result = TimeSpan.FromTicks(text.To<long>());
                else result = TimeSpan.Parse(text);
            }

            return result != null;
        }
        catch
        {
            if (targetType.IsAnyOf(typeof(int), typeof(long)))
            {
                if (text.Contains(".") && text.RemoveBeforeAndIncluding(".", caseSensitive: true).All(x => x == '0'))
                    result = text.RemoveFrom(".").To(actualTargetType);
            }

            if (isNullable)
                return true;
            else
                throw; // Although it is a try method, it is ok to raise an exception.
        }
    }

    #endregion
    
}
