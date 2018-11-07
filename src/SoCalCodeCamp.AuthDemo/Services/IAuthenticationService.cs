using Microsoft.Identity.Client;
using System.Threading.Tasks;

namespace SoCalCodeCamp.AuthDemo.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> LoginAsync();
        Task LogoutAsync();
    }
}