using GigaFix.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using SukiUI.Controls;

namespace GigaFix.ViewModels;

public partial class LoginViewModel : PageViewModel
{
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(CanLogin))]
    private string login = "";

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(CanLogin))]
    private string password = "";

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