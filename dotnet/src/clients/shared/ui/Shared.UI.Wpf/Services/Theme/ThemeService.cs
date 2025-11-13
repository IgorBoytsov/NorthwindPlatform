using Shared.UI.Wpf.Enums;
using System.Windows;

namespace Shared.UI.Wpf.Services.Theme
{
    public class ThemeService : IThemeService
    {
        private const string ThemeResourcePathFormat = "/Shared.UI.Wpf;component/Resources/Theme.{0}.xaml";

        public void SetTheme(Themes theme)
        {
            var mergedDictionaries = Application.Current.Resources.MergedDictionaries;

            var currentThemeDictionary = mergedDictionaries.FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("/Resources/Theme."));

            if (currentThemeDictionary != null)
                mergedDictionaries.Remove(currentThemeDictionary);

            string themeName = theme.ToString();
            var newThemeUri = new Uri(string.Format(ThemeResourcePathFormat, themeName), UriKind.RelativeOrAbsolute);

            var newThemeDictionary = new ResourceDictionary { Source = newThemeUri };
            mergedDictionaries.Add(newThemeDictionary);

            // TODO: Сохранить выбор темы 
        }

        public Themes GetCurrentTheme()
        {
            var mergedDictionaries = Application.Current.Resources.MergedDictionaries;

            var darkThemeDictionary = mergedDictionaries.FirstOrDefault(d => d.Source != null && d.Source.OriginalString.EndsWith("Theme.Dark.xaml"));

            return darkThemeDictionary != null ? Themes.Dark : Themes.Light;
        }

        public void InitializeTheme()
        {
            // TODO: Получать сохраненую тему с файла
            SetTheme(Themes.Dark);
        }
    }
}