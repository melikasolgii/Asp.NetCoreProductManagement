namespace eShop.Catalog.Application.Contract.Data
{
    public class PageList<TEntity>
    {
        private List<TEntity> _items = new();
        public IReadOnlyList<TEntity> Items => _items.AsReadOnly();
        public int TotalRecordCount { get; set; }
        public PageList(IEnumerable<TEntity> items, int totalRecordCount)
        {

            _items.AddRange(items);
            TotalRecordCount = totalRecordCount;

        }
    }
}
