using eShop.Catalog.Application.Contract.Data;
using CSharpFunctionalExtensions;
using eShop.Catalog.Application.Products.Contracts.DTOs;

namespace eShop.Catalog.Application.Products.Contracts
{
    //Result Pattern
    public interface IProductManager
    {

        //Task CreateProductAsync(ProductForCreateDto product);
        // Task< IEnumerable<ProductDto>> GetProductsAsync();
        //  Task <ProductDto?> GetProductByIdAsync(Guid productId);

        Task<Result> CreateProductAsync(ProductForCreateDto productDto);
        Task<Result> UpdateProductAsync(Guid productId, ProductForUpdateDto productDto);
        Task<Result> DeleteProductAsync(Guid productId);
        Task<Result<IEnumerable<ProductDto>>> GetProductsAsync();
        Task<Result<ProductDto>> GetProductByIdAsync(Guid productId);
        //Task<Result<IEnumerable<ProductDto>>> FilterProductsAsync(string? criteria, QueryData data);

        //OoR  Task<Result<IEnumerable<ProductDto>>> FilterProductAsync(string? criteria, string? sort, int pageSize, int pageIndex);
        Task<Result<PageList<ProductDto>>> FilterProductsAsync(string? criteria, QueryData data);
        Task<Result<PageList<ProductDto>>> SearchProductAsync(string? text, QueryData data);




    }
}
