using Avalonia;
using GigaFix.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using GigaFix.Data;
using Microsoft.EntityFrameworkCore;
using SukiUI.Controls;

namespace GigaFix.ViewModels
{
    public partial class LoginViewModel : PageViewModel
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanLogin))]
        string login = ""; 
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanLogin))]
        string password = "";
        public bool CanLogin => !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password);
        
        private readonly NavigationService navigationService;
        private readonly AuthService authService;
        public LoginViewModel(NavigationService navigationService, AuthService authService)
        {
            this.navigationService = navigationService;
            this.authService = authService;
#if DEBUG
            Login = "login1@gmail.com";
            Password = "password1";
#endif
        }

        public override string Title => "Авторизуйтесь";

        public async void StartLogin()
        {
            if (await authService.Login(Login, Password))
                    navigationService.Navigate(App.GetRequiredService<MainViewModel>());
            else
                await SukiHost.ShowToast("Ошибка", "Не удалось войти");
        }
    }
}
