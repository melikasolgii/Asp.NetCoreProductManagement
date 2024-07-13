using eShop.Catalog.Domain.Primitives.Contracts;

namespace eShop.Catalog.Domain.Primitives
{
    public abstract class Entity<TId> : IEntity<TId>  // abstract نماینده مشترک همه موجودیت ها بخاطر همین 
        where TId : notnull
    {
        public TId Id { get; protected set; }

        public Entity(TId id)
        {
            Id = id;
        }
    }
}
