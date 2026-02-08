using NorthwindPlatform.Authentication.ApiClient.HttpClients;
using NorthwindPlatform.Modules.Authentication.Wpf.Views;
using Shared.Client.Security.Abstractions;
using Shared.Client.Security.Windows;

namespace NorthwindPlatform.Modules.Authentication.Wpf
{
    public sealed class AuthenticationModule(IRegionManager regionManager) : IModule
    {
        private readonly IRegionManager _regionManager = regionManager;

        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<LoginView>();
            containerRegistry.Register<IAuthenticationService, AuthService>();
            containerRegistry.RegisterSingleton<IDeviceIdentityService, DeviceIdentityService>();
        }
    }
}