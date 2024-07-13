using Carter;
using eShop.Catalog;
using eShop.Security;
using FluentValidation;


namespace eShop
{
    public static class DependencyInjection
    {
        public static void AddEShopServices(this IServiceCollection services , IConfiguration configuration , IWebHostEnvironment env)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddCarter();
            services.AddAutoMapper(typeof(DependencyInjection).Assembly);
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            services.AddCatalogServices(configuration, env);
            services.AddSecurityServices(configuration, env);


                 

        }



    }
}
