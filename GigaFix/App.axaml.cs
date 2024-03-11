using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using GigaFix.ViewModels;
using GigaFix.Views;
using Splat;
using SukiUI;
using SukiUI.Models;

namespace GigaFix
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            var MainTheme = new SukiColorTheme(
                "MainTheme",
                Color.FromRgb(253, 189, 64),
                Color.FromRgb(176, 229, 253));
            SukiTheme.GetInstance().AddColorTheme(MainTheme);
            SukiTheme.GetInstance().ChangeColorTheme(MainTheme);
            
            
        }
        

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Line below is needed to remove Avalonia data validation.
                // Without this line you will get duplicate validations from both Avalonia and CT
                BindingPlugins.DataValidators.RemoveAt(0);
                desktop.MainWindow = new MainWindow
                {
                    DataContext = App.GetRequiredService<MainWindowViewModel>(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

        public static T GetRequiredService<T>() => Locator.Current.GetService<T>();
    }
}