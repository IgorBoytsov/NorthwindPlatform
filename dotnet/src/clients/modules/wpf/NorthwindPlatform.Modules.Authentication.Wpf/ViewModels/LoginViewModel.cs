using Shared.Client.Security.Abstractions;
using Shared.Contracts.Requests.Security;
using Shared.UI.Wpf.Enums;
using System.Windows;

namespace NorthwindPlatform.Modules.Authentication.Wpf.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ISecureTokenStorage _tokenStorage;
        private readonly IRegionManager _regionManager;

        /*--Инициализация---------------------------------------------------------------------------------*/

        public LoginViewModel(
            IAuthenticationService authenticationService, 
            ISecureTokenStorage tokenStorage,
            IRegionManager regionManager)
        {
            _authenticationService = authenticationService;
            _tokenStorage = tokenStorage;
            _regionManager = regionManager;

            LoginCommand = new AsyncDelegateCommand<object>(ExecuteLogin, CanExecuteLogin);
        }

        /*--Коллекции-------------------------------------------------------------------------------------*/

        /*--Свойства--------------------------------------------------------------------------------------*/

        private string? _login;
        public string? Login
        {
            get => _login;
            set
            {
                if (SetProperty(ref _login, value))
                {
                    LoginCommand.RaiseCanExecuteChanged();
                }
            } 
        }

        private string? _password;
        public string? Password
        {
            get => _password;
            set
            {
                if (SetProperty(ref _password, value))
                {
                    LoginCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string? _errorMessage;
        public string? ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        /*--Команды---------------------------------------------------------------------------------------*/

        #region Команда [LoginCommand]: Аунтетификация

        public AsyncDelegateCommand<object> LoginCommand { get; private set; }

        private async Task ExecuteLogin(object passwordBox)
        {
            try
            {
                var result = await _authenticationService.LoginAsync(new LoginRequest(Login!, Password!));

                if (result.IsSuccess)
                {
                    await _tokenStorage.StoreTokensAsync(result.Value.AccessToken, result.Value.RefreshToken);
                    _regionManager.RequestNavigate(Regions.MainRegion.ToString(), "");
                }
                else
                    MessageBox.Show(result.StringMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла непредвиденная ошибка: {ex}");
            }
        }

        private bool CanExecuteLogin(object passwordBox) 
            => !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password);

        #endregion

        /*--Методы----------------------------------------------------------------------------------------*/
    }
}