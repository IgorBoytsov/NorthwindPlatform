using Shared.UI.Wpf.Enums;

namespace Shared.UI.Wpf.Services.Theme
{
    public interface IThemeService
    {
        void SetTheme(Themes theme);
        Themes GetCurrentTheme();
        void InitializeTheme();
    }
}