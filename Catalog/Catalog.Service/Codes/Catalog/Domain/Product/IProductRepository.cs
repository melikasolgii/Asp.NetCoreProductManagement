using eShop.Catalog.Domain.Primitives.Contracts;

namespace eShop.Catalog.Domain.Product
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
    }
}
