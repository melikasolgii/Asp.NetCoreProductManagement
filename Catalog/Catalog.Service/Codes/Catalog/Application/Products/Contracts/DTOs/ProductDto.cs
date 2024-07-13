namespace eShop.Catalog.Application.Products.Contracts.DTOs
{
    public sealed record ProductDto(Guid Id, string Name, decimal Price, string Description);
}
