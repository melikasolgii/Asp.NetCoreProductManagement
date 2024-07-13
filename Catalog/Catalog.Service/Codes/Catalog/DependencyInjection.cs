using Microsoft.EntityFrameworkCore;
using eShop.Catalog.Infrastructure.Persistence;
using eShop.Catalog.Domain.Product;
using eShop.Catalog.Infrastructure.Persistence.Repositories;
using eShop.Catalog.Application.Products;
using eShop.Catalog.Application.Products.Contracts;


namespace eShop.Catalog
{
    public static class DependencyInjection
    {
        public static void AddCatalogServices(this IServiceCollection services , IConfiguration configuration , IWebHostEnvironment env)
        {

            services.AddScoped<IProductManager, ProductManager>();
            services.AddScoped<IProductRepository, ProductSqlRepository>();

            //database connectionstring 
            services.AddDbContext<CatalogContext>(setup =>
            {
                if (env.IsDevelopment())
                {
                    var cnnstr = configuration.GetConnectionString("Catalog");
                    setup.UseSqlServer(cnnstr);

                }
                else
                {
                    //
                }

            });

        }



    }
}
