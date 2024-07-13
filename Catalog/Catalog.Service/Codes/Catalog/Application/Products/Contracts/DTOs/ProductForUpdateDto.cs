using System.Reflection.PortableExecutable;

namespace eShop.Catalog.Application.Products.Contracts.DTOs
{

    //record 

    public sealed record ProductForUpdateDto(string Name, decimal Price, string Description);

}
