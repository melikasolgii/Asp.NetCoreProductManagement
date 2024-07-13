using CSharpFunctionalExtensions;
using eShop.Security.Domain.Users;
using System.Security.Claims;

namespace eShop.Security.Application.Contracts
{
    public interface IIdentityService
    {
        Task<Result<User>>FindByNameAsync(string userName);
        Task<Result> RegisterAsync(User user , string password );
        Task<Result> AddClaimsAsync(User user, IEnumerable<Claim> claims);
        Task<Result> CheckPasswordAsync(User user, string password);
        Task<Result<IEnumerable<Claim>>> GetClaimsAsync(User user);
        Result<string> GenerateIdTokenAsync(IEnumerable<Claim> claims,DateTime expire);
    }
}
