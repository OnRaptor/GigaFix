using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GigaFix.Services;
using GigaFix.ViewModels.Dialogs;
using GigaFix.ViewModels.MainViews;
using SukiUI.Controls;

namespace GigaFix.ViewModels
{
    public class MainViewModel : PageViewModel
    {
        public override string Title => "Главный экран";

        public List<PageViewModel> Views { get; set; } = new ();

        private readonly NavigationService _navigationService;
        private readonly LoginViewModel _loginViewModel;
        private readonly NotificationsViewModel _notificationViewModel;
        public MainViewModel(
            NavigationService navigationService,
            OrdersListViewModel ordersListViewModel,
            AddOrderViewModel addOrderViewModel,
            StatisticViewModel statisticViewModel,
            LoginViewModel loginViewModel,
            NotificationsViewModel notificationsViewModel
            )
        {
            OnNavigate = () => navigationService.SetTopPanelVisibility(false);
            Views.Add(ordersListViewModel);
            Views.Add(addOrderViewModel);
            Views.Add(statisticViewModel);
            _navigationService = navigationService;
            _loginViewModel = loginViewModel;
            _notificationViewModel = notificationsViewModel;
        }

        public void SignOut()
        {
            _navigationService.Navigate(_loginViewModel);
        }

        public void OpenNotifications()
        {
            _navigationService.SetTopPanelVisibility(true);
            _navigationService.Navigate(_notificationViewModel);
        }
    }
}
