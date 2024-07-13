using System.Reflection.PortableExecutable;

namespace eShop.Catalog.Application.Products.Contracts.DTOs
{
    //  public class ProductForCreateDto
    // {
    //    public ProductForCreateDto(string name, decimal price, string description)
    //    {
    //        Name = name;
    //        Price = price;
    //        Description = description;
    //    }

    //    public string Name { get; set; }
    //    public decimal Price { get; set; }
    //    public string Description { get; set; }
    //}




    //record 

    public sealed record ProductForCreateDto(string Name, decimal Price, string Description);

}
