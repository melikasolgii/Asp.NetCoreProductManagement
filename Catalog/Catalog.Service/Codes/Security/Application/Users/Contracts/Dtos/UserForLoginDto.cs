namespace eShop.Security.Application.Users.Contracts.Dtos
{
    public sealed record UserForLoginDto(
        string UserName,
        string Password
        );
   
}
