using Microsoft.Identity.Client;
using System.Linq;
using System.Threading.Tasks;

namespace SoCalCodeCamp.AuthDemo.Services
{
    /* You are not meant to understand this */
    public class AuthenticationService : IAuthenticationService
    {
        IAADOptions _options { get; }
        IPublicClientApplication _client { get; }
        UIParent _uiParent { get; }

        public AuthenticationService(IPublicClientApplication client, IAADOptions options, UIParent uiParent)
        {
            _client = client;
            _options = options;
            _uiParent = uiParent;
        }

        public async Task<AuthenticationResult> LoginAsync()
        {
            var accounts = await _client.GetAccountsAsync();
            return await _client.AcquireTokenAsync(_options.Scopes, accounts.FirstOrDefault(), _uiParent);
        }

        public async Task LogoutAsync()
        {
            var accounts = await _client.GetAccountsAsync();
            foreach (var account in accounts)
                await _client.RemoveAsync(account);
        }
    }
}