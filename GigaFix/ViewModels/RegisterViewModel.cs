using GigaFix.Services;

namespace GigaFix.ViewModels;

public class RegisterViewModel : PageViewModel
{
    public override string Title => "Регистрация";

    private readonly NavigationService _navigationService;

    public RegisterViewModel(NavigationService _navigationService)
    {
        this._navigationService = _navigationService;
    }

    public void Register()
    {
        _navigationService.Navigate(App.GetRequiredService<MainViewModel>());
    }
}