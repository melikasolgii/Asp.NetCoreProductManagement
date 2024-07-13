using eShop.Security.Infrastructure.Services.Authorization.Options;
using eShop.Security.Domain.Roles;
using eShop.Security.Domain.Users;
using eShop.Security.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using eShop.Security.Application.Users.Contracts;
using eShop.Security.Application.Users;
using eShop.Security.Application.Contracts;
using eShop.Security.Infrastructure.Services.Authentication;


namespace eShop.Security
{
    public static class DependencyInjection
    {
        public static void AddSecurityServices(
            this IServiceCollection services ,
            IConfiguration configuration ,
            IWebHostEnvironment env)
        {
 
            services.AddCors();
            services.ConfigureOptions<CorsOptionsSetup>();

            //database connectionstring 
            services.AddDbContext<SecurityContext>(setup =>
            {
                if (env.IsDevelopment())
                {
                    var cnnstr = configuration.GetConnectionString("Security");
                    setup.UseSqlServer(cnnstr);

                }
                else
                {
                    //
                }

            })
            .AddIdentity<User,Role>()
            .AddEntityFrameworkStores<SecurityContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IIdentityService,IdentityService>();

        }



    }
}
