using System;
using Prism.DryIoc;
using DryIoc;
using Prism.Ioc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism;
using SoCalCodeCamp.AuthDemo.Sample.Helpers;
using SoCalCodeCamp.AuthDemo.Services;
using Prism.Modularity;
using Prism.Logging;
using System.Collections.Generic;
using Prism.Events;
using SoCalCodeCamp.AuthDemo.Events;
using Microsoft.Identity.Client;
using Prism.Navigation;
using SoCalCodeCamp.AuthDemo.Sample.Views;
using SoCalCodeCamp.AuthDemo.Sample.ViewModels;
using System.Diagnostics;
using Prism.Logging.Syslog;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SoCalCodeCamp.AuthDemo.Sample
{
    public partial class App
    {
        public App() : this(null) { }

        public App(IPlatformInitializer platformInitializer) : base(platformInitializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            var ea = Container.Resolve<IEventAggregator>().GetEvent<UserAuthenticatedEvent>().Subscribe(OnUserAuthenticated);
            var result = await NavigationService.NavigateAsync("LoginPage");

            if(!result.Success)
            {
                Debugger.Break();
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.Register<IAADOptions, SampleAADOptions>();

            // Logging Setup
            containerRegistry.RegisterSingleton<ISyslogOptions, SyslogLoggerOptions>();
            containerRegistry.GetContainer().RegisterMany<SyslogLogger>(Reuse.Singleton,
                serviceTypeCondition: t => typeof(SyslogLogger).ImplementsServiceType(t));
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<AuthModule>();
        }

        protected override void InitializeModules()
        {
            // Hack: Listen for exceptions thrown during Initialization
            var manager = Container.Resolve<IModuleManager>();
            manager.LoadModuleCompleted += OnLoadModuleCompleted;
            base.InitializeModules();
            manager.LoadModuleCompleted -= OnLoadModuleCompleted;
        }

        private void OnLoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
            if(e.Error != null)
            {
                System.Diagnostics.Debugger.Break();
                Container.Resolve<ILogger>().Report(e.Error, new Dictionary<string, string>
                {
                    { "ModuleName", e.ModuleInfo.ModuleName }
                });
            }
        }

        private void OnUserAuthenticated(AuthenticationResult result)
        {
            NavigationService.NavigateAsync("/MainPage", new NavigationParameters { { "authResult", result } });
        }
    }
}
