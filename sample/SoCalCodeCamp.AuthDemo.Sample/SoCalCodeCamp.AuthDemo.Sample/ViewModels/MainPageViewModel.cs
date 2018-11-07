using Microsoft.Identity.Client;
using Microsoft.IdentityModel.JsonWebTokens;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SoCalCodeCamp.AuthDemo.Sample.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigatingAware
    {
        public MainPageViewModel()
        {
            Claims = new ObservableCollection<string>();
        }

        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public ObservableCollection<string> Claims { get; set; }

        public void OnNavigatingTo(INavigationParameters parameters)
        {
            var result = parameters.GetValue<AuthenticationResult>("authResult");
            Username = result?.Account.Username;

            var name = new string[2];
            foreach (var claim in new JsonWebToken(result.AccessToken).Claims)
            {
                switch(claim.Type)
                {
                    case "given_name":
                        name[0] = claim.Value;
                        break;
                    case "family_name":
                        name[1] = claim.Value;
                        break;
                }

                Claims.Add($"{claim.Type}: {claim.Value}");
            }

            if (string.IsNullOrEmpty(Username))
                Username = string.Join(" ", name);
        }
    }
}
