using CSharpFunctionalExtensions;
using eShop.Security.Application.Contracts;
using eShop.Security.Application.Users;
using eShop.Security.Domain.Users;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eShop.Security.Infrastructure.Services.Authentication
{
    public class IdentityService(UserManager<User> userManager, IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory ) : IIdentityService
    {
        //? Options Pattern
        private const string SECRET = "eShop Application 0209 eShop Application 0209 eShop Application 0209";

        public async Task<Result> AddClaimsAsync(User user, IEnumerable<Claim> claims)
        {
            var AddClaimsResult =await userManager.AddClaimsAsync(user, claims);
            if (AddClaimsResult.Succeeded)
                return Result.Success();
            else
                return Result.Failure(
                    string.Join(",", AddClaimsResult.Errors.Select(e => e.Description))
                );
           
        }

        public async Task<Result> CheckPasswordAsync(User user, string password)
        {
            var isValidPassword = await userManager.CheckPasswordAsync(user, password);

            if (isValidPassword)
                return Result.Success();
            else
                return Result.Failure("UserName or Password is incorrect");
        }

        public async Task<Result<User>> FindByNameAsync(string userName)
        {
            //return Task.FromResult(Result.Failure<User>($"userName({userName} not found"));
            var user = await userManager.FindByNameAsync(userName);

            if (user is not null)
                return user;
            else
                return Result.Failure<User>($"UserName ({userName}) not found");

        }

        public  Result<string> GenerateIdTokenAsync(IEnumerable<Claim> claims, DateTime expire)
        {
            //return Task.FromResult(Result.Success("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c"));

            //? Sign (Key + Hash)
            //? Symmetric, Asymmetric
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET)),
                SecurityAlgorithms.HmacSha256
            );

            //creating token
            var securityToken = new JwtSecurityToken(
                issuer: "localhost:5001",
                audience: "localhost:4200",
                expires: expire,
                signingCredentials:null,
                claims:claims
                );
             var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }

        public async Task<Result<IEnumerable<Claim>>> GetClaimsAsync(User user)
        {
            var principal = await userClaimsPrincipalFactory.CreateAsync(user);

            return Result.Success(principal.Claims);
        }

      

        public async Task<Result> RegisterAsync(User user, string password)
        {
            //return Task.FromResult(Result.Success());
            var registerResult = await userManager.CreateAsync(user, password);

            if (registerResult.Succeeded)
                return Result.Success();
            else
                return Result.Failure(
                    string.Join(",", registerResult.Errors.Select(e => e.Description))
                );

        }
    }
}
