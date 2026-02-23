using Shared.UI.Wpf;
using Shared.UI.Wpf.Enums;
using Shared.UI.Wpf.Services.Theme;
using System.Windows;

namespace NorthwindPlatform.Wpf.Shell.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IThemeService _themeService;

        /*--Инициализация---------------------------------------------------------------------------------*/

        public MainWindowViewModel(IThemeService themeService)
        {
            _themeService = themeService;
            var startTheme = Themes.Dark;
            _themeService.SetTheme(startTheme);
            SelectedTheme = ApplicationThemes.FirstOrDefault(t => t.Key == startTheme);

            InitializeCommand();
        }

        private void InitializeCommand()
        {
            static bool alwaysExecute(Window wnd) => true;

            ShutDownCommand = new DelegateCommand<Window>(Execute_ShutDown, alwaysExecute);
            MinimizeCommand = new DelegateCommand<Window>(Execute_Minimize, alwaysExecute);
            MaximizeCommand = new DelegateCommand<Window>(Execute_Maximize, alwaysExecute);
            RestoreCommand = new DelegateCommand<Window>(Execute_Restore, alwaysExecute);

            SwitchTheme = new DelegateCommand<Themes?>(Execute_SwitchTheme, Can_SwitchTheme);
        }

        /*--Коллекции-------------------------------------------------------------------------------------*/

        public List<KeyValuePair<Themes, string>> ApplicationThemes { get; private init; } =
        [
           new KeyValuePair<Themes, string>(Themes.Dark, "Темная"),
           new KeyValuePair<Themes, string>(Themes.Light, "Светлая"),
        ];

        /*--Свойства--------------------------------------------------------------------------------------*/

        public PopupController UserMenuPopup { get; } = new PopupController(staysOpen: false, placementMode: PopupPlacementMode.BottomCenter);

        private string _title = "Northwind Platform";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private KeyValuePair<Themes, string> _selectedTheme;
        public KeyValuePair<Themes, string> SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                if (SetProperty(ref _selectedTheme, value))
                {
                    _themeService.SetTheme(value.Key);
                }
            }
        }

        /*--Команды---------------------------------------------------------------------------------------*/

        public DelegateCommand<Window>? ShutDownCommand { get; private set; }

        private void Execute_ShutDown(Window wnd) => wnd.Close();

        public DelegateCommand<Window>? MinimizeCommand { get; private set; }

        private void Execute_Minimize(Window wnd) => SystemCommands.MinimizeWindow(wnd);

        public DelegateCommand<Window>? MaximizeCommand { get; private set; }

        private void Execute_Maximize(Window wnd) => SystemCommands.MaximizeWindow(wnd);

        public DelegateCommand<Window>? RestoreCommand { get; private set; }

        private void Execute_Restore(Window wnd) => SystemCommands.RestoreWindow(wnd);

        public DelegateCommand<Themes?>? SwitchTheme { get; private set; }

        private void Execute_SwitchTheme(Themes? theme)
        {
            _themeService.SetTheme(theme!.Value);
            SwitchTheme?.RaiseCanExecuteChanged();
        }

        private bool Can_SwitchTheme(Themes? theme)
        {
            //if(_themeService is not null)
                return theme != _themeService.GetCurrentTheme();

            //return true;
        }

        /*--Методы----------------------------------------------------------------------------------------*/

    }
}