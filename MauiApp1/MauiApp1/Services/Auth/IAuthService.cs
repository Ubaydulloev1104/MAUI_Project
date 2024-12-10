namespace MauiApp1.Services.Auth
{
    public interface IAuthService
    {
        Task<string> RegisterUserAsync(RegisterUserCommand command);
        Task<string> LoginUserAsync(LoginUserCommand command, bool newRegister = false);
        Task<HttpResponseMessage> ChangePassword(ChangePasswordUserCommand command);
        Task<HttpResponseMessage> CheckUserName(string userName);
        Task<HttpResponseMessage> CheckUserDetails(CheckUserDetailsQuery checkUserDetailsQuery);
    }
}
