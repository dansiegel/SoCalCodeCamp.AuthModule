using Microsoft.Identity.Client;
using Prism.AppModel;
using Prism.Events;
using Prism.Logging;
using Prism.Services;
using ReactiveUI;
using SoCalCodeCamp.AuthDemo.Events;
using SoCalCodeCamp.AuthDemo.i18n;
using SoCalCodeCamp.AuthDemo.Services;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace SoCalCodeCamp.AuthDemo.ViewModels
{
    // This comment is self explanatory.
    public class LoginPageViewModel : ReactiveObject, IPageLifecycleAware
    {
        private IAuthenticationService _authenticationService { get; }
        private IEventAggregator _eventAggregator { get; }
        private ILogger _logger { get; }
        private IPageDialogService _pageDialogService { get; }

        public LoginPageViewModel(IAuthenticationService authenticationService, IEventAggregator eventAggregator, ILogger logger, IPageDialogService pageDialogService)
        {
            _authenticationService = authenticationService;
            _eventAggregator = eventAggregator;
            _logger = logger;
            _pageDialogService = pageDialogService;

            _isBusyHelper = this.WhenAnyObservable(x => x.LoginCommand.IsExecuting).ToProperty(this, x => x.IsBusy, false);
            LoginCommand = ReactiveCommand.CreateFromTask(OnLoginCommandExecuted, this.WhenAnyValue(x => x.IsBusy).Select(x => !x));
        }

        private ObservableAsPropertyHelper<bool> _isBusyHelper;
        public bool IsBusy => _isBusyHelper.Value;

        public ReactiveCommand<Unit, Unit> LoginCommand { get; }

        private async Task OnLoginCommandExecuted()
        {
            try
            {
                // If you're reading this, that means you have been put in charge of my previous project.
                // I am so, so sorry for you. God speed.
                var result = await _authenticationService.LoginAsync();
                
                if(result is null)
                {
                    var up = new Exception("Something is really wrong");
                    throw up; // that's gross...
                }

                _eventAggregator.GetEvent<UserAuthenticatedEvent>().Publish(result);
            }
            catch (Exception ex)
            {
                if (ex is MsalClientException msal && msal.ErrorCode == MsalClientException.AuthenticationCanceledError) return;

                await _pageDialogService.DisplayAlertAsync(AppResources.Error, ex.Message, AppResources.Ok);
                _logger.Report(ex);
            }
            
        }

        public void OnAppearing() => LoginCommand.Execute();

        public void OnDisappearing() { }
    }
}