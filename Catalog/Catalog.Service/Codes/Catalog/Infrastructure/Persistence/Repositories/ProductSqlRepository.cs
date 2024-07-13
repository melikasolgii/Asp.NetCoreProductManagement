using System.Linq.Dynamic.Core;
using eShop.Catalog.Domain.Product;
using eShop.Catalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eShop.Catalog.Infrastructure.Persistence.Repositories;

public class ProductSqlRepository : SqlRepository<Product, Guid>, IProductRepository
{
    public ProductSqlRepository(CatalogContext context)
        : base(context) { }

    //? Specific Search
    public override async Task<(IEnumerable<Product> Entities, int TotalRecordCount)> SearchAsync(
        string? text,
        string? sort,
        int pageSize,
        int pageIndex
    )
    {
        var query = set.AsNoTracking();

        if (!string.IsNullOrEmpty(text))
        {
            //? Search
            //? select * from Products where Name like '%mob%'
            query = query.Where(product =>
                EF.Functions.Like(product.Name, $"%{text}%")
                || EF.Functions.Like(product.Description, $"%{text}%")
            );
        }

        if (!string.IsNullOrEmpty(sort))
        {
            query = query.OrderBy(sort);
        }

        //? select count(*) from products where ...
        var entities = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

        var totalRecordCount = await query.CountAsync();

        return (entities, totalRecordCount);
    }
}
