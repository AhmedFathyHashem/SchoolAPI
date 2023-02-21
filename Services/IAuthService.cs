using Schools.Models;

namespace Schools.Services
{
    public interface IAuthService
    {
        Task<AuthModel> Registeration(RegisterModel model);
        Task<AuthModel> SignIn(SignInModel model);
    }
}
