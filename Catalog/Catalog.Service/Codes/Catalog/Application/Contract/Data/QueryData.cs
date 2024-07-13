namespace eShop.Catalog.Application.Contract.Data
{
    public sealed record QueryData(string? sort, int pageSize, int pageIndex);

}
