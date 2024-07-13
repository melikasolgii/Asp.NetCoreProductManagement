namespace eShop.Catalog.Presentaion.Requests
{
    public sealed record SearchRequest(string? Text, string? sort, int pageSize, int pageIndex);

}
