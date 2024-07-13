namespace eShop.Catalog.Domain.Product
{
    public class Name
    {
        public string Value { get; private set; }
        public Name(string value)
        {
            Value = value;
        }
    }
}
