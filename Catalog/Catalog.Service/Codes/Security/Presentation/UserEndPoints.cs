using Carter;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using eShop.Security.Application.Users.Contracts;
using eShop.Catalog.Application.Products.Contracts;
using eShop.Catalog.Presentaion.Requests;
using eShop.Security.Application.Users.Contracts.Dtos;


namespace eShop.Security.Presentaion
{
    public class UserEndPoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var users = app.MapGroup("users").WithTags("users");
            users.MapPost("/register", RegisterAsync);
            users.MapPost("/login", LoginAsync);
        }



        private async Task<IResult> RegisterAsync(
            [FromBody] UserForRegisterDto registerDto,
            [FromServices] IUserManager userManager
            )
        {
            var result = await userManager.RegisterAsync(registerDto);
            if(result.IsSuccess)
            {
                return Results.Ok();
            }

            return Results.BadRequest(result.Error);
            
        }

        private async Task<IResult> LoginAsync(
            [FromBody] UserForLoginDto loginDto,
            [FromServices] IUserManager userManager
            )
        {
            var result = await userManager.LoginAsync(loginDto);

            if (result.IsSuccess)
            {
                return Results.Ok(result.Value);
            }

            //var validationResult = JsonSerializer.Deserialize<ValidationResult>(result.Error);
            //if (validationResult is not null)
            //{
            //    return Results.ValidationProblem(validationResult!.ToDictionary());
            //}
            return Results.BadRequest(result.Error);

        }




    }
}
