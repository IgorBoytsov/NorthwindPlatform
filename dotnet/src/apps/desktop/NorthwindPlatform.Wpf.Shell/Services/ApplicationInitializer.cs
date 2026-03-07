using NorthwindPlatform.Modules.Authentication.Wpf.Views;
using Quantropic.Security.Abstractions;
using Shared.Client.Security.Abstractions;
using Shared.Contracts.Requests.Security;
using Shared.UI.Wpf.Enums;
using System.Windows;

namespace NorthwindPlatform.Wpf.Shell.Services
{
    public interface IApplicationInitializer
    {
        Task InitializeAsync();
    }

    public sealed class ApplicationInitializer(
        ISecureTokenStorage tokenStorage,
        IAuthenticationService authService,
        IRegionManager regionManager) : IApplicationInitializer
    {
        private readonly ISecureTokenStorage _tokenStorage = tokenStorage;
        private readonly IAuthenticationService _authService = authService;
        private readonly IRegionManager _regionManager = regionManager;

        public async Task InitializeAsync()
        {
            var refreshToken = await _tokenStorage.GetRefreshTokenAsync();

            if (!string.IsNullOrEmpty(refreshToken))
            {
                var result = await _authService.LoginByTokenAsync(new LoginByTokenRequest(refreshToken));

                if (result.IsFailure)
                {
                    MessageBox.Show(result.StringMessage);
                    return;
                }

                await _tokenStorage.StoreTokensAsync(result.Value.AccessToken, result.Value.RefreshToken);

                _regionManager.RequestNavigate(Regions.MainRegion.ToString(), "");
            }
            else _regionManager.RequestNavigate(Regions.MainRegion.ToString(), nameof(LoginView));

        }
    }
}