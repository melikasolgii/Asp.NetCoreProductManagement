namespace eShop.Security.Application.Users.Contracts.Dtos
{
    public sealed record UserForRegisterDto(
        string UserName,
        string Password ,
        string Email,
        string FirstName,
        string LastName
        );
   
}
