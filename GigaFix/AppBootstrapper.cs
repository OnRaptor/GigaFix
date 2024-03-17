using GigaFix.Services;
using GigaFix.ViewModels;
using System;
using GigaFix.Data;
using GigaFix.ViewModels.Dialogs;
using GigaFix.ViewModels.MainViews;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Splat.Microsoft.Extensions.DependencyInjection;

namespace GigaFix;

public class AppBootstrapper
{
    public static IServiceProvider RegisterAll()
    {
        var services = new ServiceCollection();
        services.UseMicrosoftDependencyResolver();
        services.AddLogging(builder => builder.AddDebug().AddConsole());
        services.AddDbContext<AppDbContext>(ServiceLifetime.Transient);
        services.AddSingleton<NavigationService>();
        services.AddSingleton<AuthService>();
        services.AddSingleton<ApplicationsService>();
        services.AddSingleton<NotificationsService>();
        services.AddTransient<LoginViewModel>();
        services.AddTransient<RegisterViewModel>();
        services.AddTransient<AttachExecutorViewModel>();
        services.AddTransient<EditOrderViewModel>();
        services.AddSingleton<NotificationsViewModel>();
        services.AddTransient<OrdersListViewModel>();
        services.AddTransient<AddOrderViewModel>();
        services.AddTransient<StatisticViewModel>();
        services.AddTransient<MainViewModel>();
        services.AddTransient<MainWindowViewModel>();
        IServiceProvider container = services.BuildServiceProvider();
        container.UseMicrosoftDependencyResolver();
        return container;
    }
}