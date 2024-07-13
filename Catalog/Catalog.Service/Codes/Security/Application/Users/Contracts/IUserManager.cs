using CSharpFunctionalExtensions;
using eShop.Security.Application.Users.Contracts.Dtos;

namespace eShop.Security.Application.Users.Contracts
{   
    //registering users and loging
    public interface IUserManager
    {
        Task<Result> RegisterAsync(UserForRegisterDto registerDto);
        Task<Result<UserTokenDto>> LoginAsync(UserForLoginDto loginDto);
    }
}
