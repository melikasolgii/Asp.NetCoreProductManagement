namespace eShop.Security.Application.Users.Contracts.Dtos
{
    //the output of loginDto 
    public sealed record UserTokenDto( 
        string IdToken, 
        string AccessToken,
        string RefreshToken,
        int ExpiresIn
        );
   

}
