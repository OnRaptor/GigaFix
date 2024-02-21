using GigaFix.Services;
using GigaFix.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaFix
{
    public class AppBootstrapper
    {
        public static void RegisterAll()
        {
            Locator.CurrentMutable.RegisterConstant(new NavigationService());
            Locator.CurrentMutable.Register(() => new LoginViewModel(
                Locator.Current.GetService<NavigationService>()
                ));

            Locator.CurrentMutable.Register(() => new MainViewModel());
            Locator.CurrentMutable.Register(() => new MainWindowViewModel(
                Locator.Current.GetService<NavigationService>(),
                Locator.Current.GetService<LoginViewModel>()
                ));
        }
    }
}
