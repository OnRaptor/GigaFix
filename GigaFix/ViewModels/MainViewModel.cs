using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GigaFix.Services;
using GigaFix.ViewModels.Dialogs;
using GigaFix.ViewModels.MainViews;
using Microsoft.Extensions.Logging;
using SukiUI.Controls;

namespace GigaFix.ViewModels
{
    public partial class MainViewModel : PageViewModel, IDisposable
    {
        public override string Title => "Главный экран";
        public List<PageViewModel> Views { get; set; } = new ();
        [ObservableProperty] private string userName; 
        public bool IsDispatcher { get; set; }
        
        [ObservableProperty]
        private object selectedView;

        private readonly NavigationService _navigationService;
        private readonly LoginViewModel _loginViewModel;
        private readonly NotificationsViewModel _notificationViewModel;
        public MainViewModel(
            NavigationService navigationService,
            OrdersListViewModel ordersListViewModel,
            AddOrderViewModel addOrderViewModel,
            StatisticViewModel statisticViewModel,
            LoginViewModel loginViewModel,
            NotificationsViewModel notificationsViewModel,
            AuthService authService
            )
        {
            Views.Add(ordersListViewModel);
            if (authService.IsDispatcher)
            {
                Views.Add(addOrderViewModel);
                Views.Add(statisticViewModel);
            }

            IsDispatcher = authService.IsDispatcher;
            _navigationService = navigationService;
            _loginViewModel = loginViewModel;
            _notificationViewModel = notificationsViewModel;
            userName = authService.AuthenticatedUserName;
        }

        public void Dispose()
        {
            
        }

        partial void OnSelectedViewChanged(object view)
        {
            if (view is PageViewModel)
            {
                var vm = view as PageViewModel;
                if (vm.OnNavigate != null)
                    vm.OnNavigate();
            }
        }
        public override Action? OnNavigate => () => _navigationService.SetTopPanelVisibility(false);

        public void SignOut()
        {
            _navigationService.Navigate(_loginViewModel);
            this.Dispose();
        }

        public void OpenNotifications()
        {
            _navigationService.SetTopPanelVisibility(true);
            _navigationService.Navigate(_notificationViewModel);
        }
    }
}
