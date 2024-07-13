using AutoMapper;
using eShop.Catalog.Application.Products.Contracts.DTOs;
using eShop.Catalog.Domain.Product;

namespace eShop.Catalog.Application.Products
{
    public class productMapper : Profile
    //mappers are used to transform and converting object to object
    {

        public productMapper()
        {
            //convert product to productDto
            CreateMap<Product, ProductDto>();

        }





    }
}
