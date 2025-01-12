using MauiTheming.Resources.Theme;

namespace MauiTheming
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            CheckAndChangeAppTheme(appTheme: AppTheme.Unspecified);
            RequestedThemeChanged += OnAppThemeChanged;
        }

        private void OnAppThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            CheckAndChangeAppTheme(e.RequestedTheme);
        }

        private void CheckAndChangeAppTheme(AppTheme appTheme)
        {
            var mergedDictionaries = Resources.MergedDictionaries;

            if (mergedDictionaries == null)
            {
                return;
            }

            // Remove existing LightTheme or DarkTheme if present
            var existingTheme = mergedDictionaries.FirstOrDefault(
                dict => dict.GetType() == typeof(LightTheme) || dict.GetType() == typeof(DarkTheme));

            if (existingTheme != null)
            {
                mergedDictionaries.Remove(existingTheme);
            }

            // Add the appropriate theme based on the appTheme
            if (appTheme == AppTheme.Light || appTheme == AppTheme.Unspecified)
            {
                mergedDictionaries.Add(new LightTheme());
            }
            else
            {
                mergedDictionaries.Add(new DarkTheme());
            }
        }
    }
}
