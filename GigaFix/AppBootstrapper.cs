using GigaFix.Services;
using GigaFix.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GigaFix.ViewModels.MainViews;

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
            Locator.CurrentMutable.Register(() => new RegisterViewModel(
                Locator.Current.GetService<NavigationService>()
            ));
            Locator.CurrentMutable.Register(() => new OrdersListViewModel());
            Locator.CurrentMutable.Register(() => new AddOrderViewModel());
            Locator.CurrentMutable.Register(() => new SearchViewModel());
            Locator.CurrentMutable.Register(() => new StatisticViewModel());
            Locator.CurrentMutable.Register(() => new MainViewModel(
                Locator.Current.GetService<NavigationService>(),
                Locator.Current.GetService<OrdersListViewModel>(),
                Locator.Current.GetService<AddOrderViewModel>(),
                Locator.Current.GetService<SearchViewModel>(),
                Locator.Current.GetService<StatisticViewModel>()
                ));
            Locator.CurrentMutable.Register(() => new MainWindowViewModel(
                Locator.Current.GetService<NavigationService>(),
                Locator.Current.GetService<LoginViewModel>()
                ));
        }
    }
}
