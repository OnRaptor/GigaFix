using Avalonia;
using GigaFix.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaFix.ViewModels
{
    public class LoginViewModel : PageViewModel
    {
        private readonly NavigationService navigationService;
        public LoginViewModel(NavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public override string Title => "Авторизуйтесь";

        public void Login()
        {
            navigationService.Navigate(App.GetRequiredService<MainViewModel>());
        }

        public void Register() => navigationService.Navigate(App.GetRequiredService<RegisterViewModel>());
    }
}
