using System.Collections.Concurrent;
using System.Reflection;

namespace OrdersService.Domain.Utils;

public class MatchType<T>
{
    static readonly ConcurrentDictionary<Type, Lazy<List<PropertyInfo>>> _properties
        = new ConcurrentDictionary<Type, Lazy<List<PropertyInfo>>>();

    static MatchType()
    {
        LoadProperties();
    }

    public static bool Match(T left, T right, params string[] ignoredProperties)
    {
        var match = true;
        var properties = _properties[typeof(T)].Value;

        foreach (var property in properties.Where(_ => !ignoredProperties.Contains(_.Name)))
        {
            var leftValue = property.GetValue(left);
            var rightValue = property.GetValue(right);

            if (leftValue == null && rightValue != null)
            {
                match = false;
            }

            if (leftValue != null && rightValue == null)
            {
                match = false;
            }

            if (leftValue != null && rightValue != null)
            {
                match &= leftValue.Equals(rightValue);
            }
        }

        return match;
    }

    private static void LoadProperties() =>
        _properties.GetOrAdd(typeof(T),
            x => new Lazy<List<PropertyInfo>>(() =>
            {
                var sourceType = typeof(T);
                if (typeof(T).IsConstructedGenericType)
                {
                    sourceType = typeof(T).GenericTypeArguments[0];
                }

                return sourceType
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance |
                                    BindingFlags.DeclaredOnly).ToList();
            }));
    }
