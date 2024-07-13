namespace eShop.Catalog.Domain.Primitives.Contracts
{
    public interface IEntity<TId>
    where TId : notnull

    {
        TId Id { get; }  //id is permanantly .shouldnt change
    }
}
