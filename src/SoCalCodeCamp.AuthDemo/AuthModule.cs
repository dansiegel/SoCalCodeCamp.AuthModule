using Microsoft.Identity.Client;
using Prism.Ioc;
using Prism.Logging;
using Prism.Modularity;
using ReactiveUI;
using SoCalCodeCamp.AuthDemo.Services;
using SoCalCodeCamp.AuthDemo.ViewModels;
using SoCalCodeCamp.AuthDemo.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms.Xaml;

//Dear future me. Please forgive me.
//I can't even begin to express how sorry I am.
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SoCalCodeCamp.AuthDemo
{
    public class AuthModule : IModule, IObserver<Exception>
    {
        private ILogger Logger { get; set; }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            Logger = containerProvider.Resolve<ILogger>();
            RxApp.DefaultExceptionHandler = this;

            var assemblyName = typeof(Xamarin.Forms.Application).Assembly.GetName();
            System.Diagnostics.Debug.WriteLine(assemblyName.Version.ToString());
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var options = ((IContainerProvider)containerRegistry).Resolve<IAADOptions>();
            var authority = $"https://login.microsoftonline.com/tfp/{options.Tenant}/{options.Policy}";

            containerRegistry.RegisterInstance<IPublicClientApplication>(new PublicClientApplication(options.ClientId, authority)
            {
                RedirectUri = $"msal{options.ClientId}://auth"
            });
            containerRegistry.Register<IAuthenticationService, AuthenticationService>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
        }

        public void OnCompleted()
        {

        }

        public void OnError(Exception error)
        {
            Logger.Report(error, new Dictionary<string, string>
            {
                { "Source", "Unobserved ReactiveUI Exception" }
            });
        }

        public void OnNext(Exception value)
        {
            Logger.Report(value, new Dictionary<string, string>
            {
                { "Source", "Unobserved ReactiveUI Exception" }
            });
        }
    }
}
