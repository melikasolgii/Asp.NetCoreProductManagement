using eShop.Catalog.Domain.Product;
using eShop.Catalog.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace eShop.Catalog.Infrastructure.Persistence
{


    //? Database API
    //? Database Service Provider
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options)
            : base(options) { }

        //? Hook
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //? DbSet --> Configuration

            //modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            modelBuilder.HasDefaultSchema("Catalog");
            //? Other Configurations
        }

        //?Insert, Update, Delete, Query
        public DbSet<Product> Products => Set<Product>();
    }

}
