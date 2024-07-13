using eShop.Security.Domain.Roles;
using eShop.Security.Domain.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eShop.Security.Infrastructure.Persistance
{
    public class SecurityContext:IdentityDbContext<User,Role,Guid>
    {
        public SecurityContext(DbContextOptions<SecurityContext> options) : base(options)
        { 
        
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //? DbSet --> Configuration

            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("Security");
          
        }
    }
}
