using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.Security.Infrastructure.Services.Authorization.Options
{
    public class CorsOptionsSetup : IConfigureOptions<CorsOptions>
    {
        private readonly IWebHostEnvironment _environment;
        public CorsOptionsSetup(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public void Configure(CorsOptions options)
        {
            options.AddDefaultPolicy(policy =>
            {

                //   policy.AllowAnyHeader();
                //   policy.AllowAnyMethod();
                //   policy.AllowAnyOrigin();


                //for development environment
                if (_environment.IsDevelopment())
                {
                    policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-TotalRecordCount");
                }
                else
                {
                    //production environment

                    policy
                    .WithOrigins("http://localhost:4200/product-list")
                    .WithMethods("Get", "Post", "Put", "Delete")
                    .WithHeaders("Header name1", "Header name2")
                    .WithExposedHeaders("X-TotalRecordCount");
                }






            });
        }

    }
}