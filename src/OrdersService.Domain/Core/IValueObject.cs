namespace OrdersService.Domain.Core;

public interface IValueObject<TValueObject> 
    where TValueObject : class
{
    bool SameIdentityAs(TValueObject other);
}

