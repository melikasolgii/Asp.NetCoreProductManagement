namespace eShop.Catalog.Presentaion.Requests
{
    public sealed record FilterRequest(string? criteria, string? sort, int pageSize, int pageIndex);

}
