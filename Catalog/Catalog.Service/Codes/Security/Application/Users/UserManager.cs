using CSharpFunctionalExtensions;
using eShop.Security.Application.Contracts;
using eShop.Security.Application.Users.Contracts;
using eShop.Security.Application.Users.Contracts.Dtos;
using eShop.Security.Domain.Users;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Linq.Expressions;
using System.Security.Claims;

namespace eShop.Security.Application.Users
{
    public class UserManager(IIdentityService identityService) : IUserManager
    {
        private const int TOKEN_EXPIRE_IN =3600;
        public async Task<Result<UserTokenDto>> LoginAsync(UserForLoginDto loginDto)
        {
            // var token = new UserTokenDto("", "", "", 1000);

            // return Task.FromResult(Result.Success<UserTokenDto>(token));
            //? Validations

            var userFindResult = await identityService.FindByNameAsync(loginDto.UserName);
            if (userFindResult.IsFailure)
            {
                return Result.Failure<UserTokenDto>(userFindResult.Error);
            }

            var checkPasswordResult = await identityService.CheckPasswordAsync(
                userFindResult.Value,
                loginDto.Password
            );

            if (checkPasswordResult.IsFailure)
            {
                return Result.Failure<UserTokenDto>(checkPasswordResult.Error);
            }

            //? Generate fake Token

            return await GenerateToken(userFindResult.Value);
        }
        private async Task<Result<UserTokenDto>> GenerateToken(User user)
        {
            //var token = new UserTokenDto(user.UserName, user.Email!, "ref token", 1000);
            //return Task.FromResult(Result.Success<UserTokenDto>(token));

            var claimResult = await identityService.GetClaimsAsync(user);
            if(claimResult.IsFailure)
            {
                return Result.Failure<UserTokenDto>(claimResult.Error);
            }

            var expire= DateTime.Now.AddSeconds(TOKEN_EXPIRE_IN);

            var tokenResult = identityService.GenerateIdTokenAsync(claimResult.Value, expire);
            if(tokenResult.IsFailure)
            {
                return Result.Failure<UserTokenDto>(tokenResult.Error);
            }
            var token = new UserTokenDto(tokenResult.Value, "", "", TOKEN_EXPIRE_IN);
            return Result.Success(token);
        }

        public async Task<Result> RegisterAsync(UserForRegisterDto registerDto)
        {
            //validators
            //making sure that username ,email  are  unique
            var UserFindResult =await identityService.FindByNameAsync(registerDto.UserName);
            if(UserFindResult.IsSuccess)
            {
                return Result.Failure($"UserName ({registerDto.UserName}) already registered");
            }
            User user = new() { UserName = registerDto.UserName, Email = registerDto.Email };
            var registerResult = await identityService.RegisterAsync(user,registerDto.Password);

            if (registerResult.IsFailure)
                return registerResult;

            //add user claims 
            var claims = new List<Claim>
            { 
                new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                new Claim(ClaimTypes.Name , user.UserName),
                new Claim(ClaimTypes.Email , user.Email),
                new Claim(ClaimTypes.GivenName , registerDto.FirstName),
                new Claim(ClaimTypes.Surname , registerDto.LastName)
            };
            var addClaimsResult = await identityService.AddClaimsAsync(user, claims);
            
            if (addClaimsResult.IsFailure)
                return addClaimsResult;
            //generate activation link and send emial

            return Result.Success();
        }
    }
}
