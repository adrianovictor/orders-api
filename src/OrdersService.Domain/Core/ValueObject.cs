using System.Reflection;
using OrdersService.Domain.Utils;

namespace OrdersService.Domain.Core;

public abstract class ValueObject<TValueObject> : IValueObject<ValueObject<TValueObject>>, IEquatable<ValueObject<TValueObject>>
    where TValueObject : class
{
    protected IEnumerable<PropertyInfo> GetProperties
        => typeof(TValueObject).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

    protected virtual IEnumerable<string> GetIgnoredProperties()
    {
        return Enumerable.Empty<string>();
    }

    public static bool operator ==(ValueObject<TValueObject> left, ValueObject<TValueObject> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(ValueObject<TValueObject> left, ValueObject<TValueObject> right)
    {
        return !Equals(left, right);
    }

    public virtual bool SameIdentityAs(ValueObject<TValueObject> other)
    {
        if (other == null)
        {
            return false;
        }

        return MatchType<TValueObject>.Match((TValueObject)(object)this, (TValueObject)(object)other, GetIgnoredProperties().ToArray());
    }

    public bool Equals(ValueObject<TValueObject> other)
    {
        return SameIdentityAs(other);
    }

    public override bool Equals(object obj)
    {
        return SameIdentityAs(obj as ValueObject<TValueObject>);
    }

    public override int GetHashCode()
    {
        var properties = typeof(TValueObject).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

        return properties
            .Select(prop => prop.GetValue(this))
            .Where(value => value != null)
            .Aggregate(0, (hashCode, value) => hashCode ^= value.GetHashCode());
    }
}
