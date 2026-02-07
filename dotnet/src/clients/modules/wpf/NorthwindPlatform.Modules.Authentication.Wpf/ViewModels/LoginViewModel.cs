using Shared.Client.Security.Abstractions;
using Shared.Contracts.Requests.AuthenticationService;
using Shared.Contracts.Requests.Security;
using Shared.UI.Wpf.Enums;
using System.Windows;

namespace NorthwindPlatform.Modules.Authentication.Wpf.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ICryptoService _cryptoService;
        private readonly ISrpService _srpService;
        private readonly ISecureTokenStorage _tokenStorage;
        private readonly IRegionManager _regionManager;

        /*--Инициализация---------------------------------------------------------------------------------*/

        public LoginViewModel(
            IAuthenticationService authenticationService, 
            ICryptoService cryptoService,
            ISrpService srpService,
            ISecureTokenStorage tokenStorage,
            IRegionManager regionManager)
        {
            _authenticationService = authenticationService;
            _cryptoService = cryptoService;
            _srpService = srpService;
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

        #region Команда [LoginCommand]: Аутентификации

        public AsyncDelegateCommand<object> LoginCommand { get; private set; }

        private async Task ExecuteLogin(object passwordBox)
        {
            try
            {
                var challengeResult = await _authenticationService.GetSrpChallenge(new SrpChallengeRequest(Login!));

                if (challengeResult.IsFailure)
                    MessageBox.Show(challengeResult.StringMessage);

                var challengeSalt = challengeResult.Value.Salt;
                var challengeB = challengeResult.Value.B;

                var (A, M1, S) = _srpService.GenerateSrpProof(Password!, challengeSalt, challengeB);

                var srpVerifyResult = await _authenticationService.VerifySrpProof(new SrpVerifyRequest(Login!, A, M1));

                if (srpVerifyResult.IsFailure)
                {
                    MessageBox.Show(srpVerifyResult.StringMessage);
                    return;
                }
                    
                var serverM2 = srpVerifyResult.Value.M2;

                var isServerValid = _srpService.VerifyServerM2(A, M1, S, serverM2!);

                if (!isServerValid)
                {
                    MessageBox.Show("Подлинность сервера не получилось подтвердить");
                    return;
                }

                var region = _regionManager.Regions[Regions.MainRegion.ToString()];

                foreach (var view in region.Views)
                    region.Remove(view);
            }
            catch (System.Exception ex)
            {
                 MessageBox.Show($"Критическая ошибка: {ex}");
            }
        }

        private bool CanExecuteLogin(object passwordBox) 
            => !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password);

        #endregion

        /*--Методы----------------------------------------------------------------------------------------*/
    }
}