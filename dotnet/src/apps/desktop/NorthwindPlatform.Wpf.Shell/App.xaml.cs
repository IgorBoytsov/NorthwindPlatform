using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NorthwindPlatform.Authentication.ApiClient.HttpClients;
using NorthwindPlatform.Modules.Authentication.Wpf;
using NorthwindPlatform.Wpf.Shell.Services;
using NorthwindPlatform.Wpf.Shell.Views;
using Quantropic.Security.Abstractions;
using Quantropic.Security.Cryptography;
using Quantropic.Security.Srp.Client;
using Quantropic.Security.Windows;
using Shared.Client.Security.Abstractions;
// using Shared.Client.Security.Windows;
// using Shared.Client.Security.Abstractions;
// using Shared.Client.Security.Cryptography;
// using Shared.Client.Security.Srp;
// using Shared.Client.Security.Windows;
using Shared.Contracts.Enums;
using Shared.UI.Wpf.Services.Theme;
using System.IO;
using System.Net.Http;
using System.Windows;

namespace NorthwindPlatform.Wpf.Shell
{
    public partial class App : PrismApplication
    {
        public static IConfiguration? Configuration { get; private set; }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var services = new ServiceCollection();
            
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("ApiConf.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();

            containerRegistry.RegisterInstance<IConfiguration>(Configuration);
            containerRegistry.RegisterSingleton<IThemeService, ThemeService>();

            containerRegistry.RegisterSingleton<IApplicationInitializer, ApplicationInitializer>();

            containerRegistry.RegisterSingleton<ISrpClient, SrpClientService>();
            containerRegistry.RegisterSingleton<ICryptoServices, CryptoService>();
            containerRegistry.RegisterSingleton<IKeyDerivationService, KeyDerivationService>();
            containerRegistry.RegisterSingleton<ISecureTokenStorage>(c => new WindowSecureTokenStorage("NorthwindPlatform"));
            containerRegistry.RegisterSingleton<IDeviceIdentityService>(c => new DeviceIdentityService("NorthwindPlatform"));

            string? authServiceApiUrl = Configuration.GetValue<string>("BaseAuthServiceUrl");
            services.AddHttpClient(ApiClientName.BaseAuthApi.ToString(), client => client.BaseAddress = new Uri(authServiceApiUrl!));

            var serviceProvider = services.BuildServiceProvider();

            containerRegistry.RegisterInstance(serviceProvider.GetRequiredService<IHttpClientFactory>());
            containerRegistry.RegisterSingleton<IAuthenticationService, AuthService>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<AuthenticationModule>();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var appInitializer = Container.Resolve<IApplicationInitializer>();

            try
            {
                _ = appInitializer.InitializeAsync();
            }
            catch (Exception) 
            {
                Application.Current.Shutdown();
                return; 
            }

            //Container.Resolve<IThemeService>().SetTheme(Themes.Dark);
        }
    }
}