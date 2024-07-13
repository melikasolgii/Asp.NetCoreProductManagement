using eShop.Catalog.Domain.Primitives;
using eShop.Catalog.Domain.Primitives;

namespace eShop.Catalog.Domain.Product
{
    public class Product : Entity<Guid>
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public string Description { get; set; }

        public Product(string name, decimal price, string description = "")
            : base(Guid.NewGuid())  //base means calling parent constructor, witch here is Entity 
        {
            Name = name;
            Price = price;
            Description = description;
        }
        public Product() : base(Guid.NewGuid())
        {

        }
        public void ChangePrice(decimal price)
        {
            //validation
            Price = price;
        }

        public void ChangeName(string name)
        {
            //validation
            Name = name;
        }
    }
}

