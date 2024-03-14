using GigaFix.Services;
using GigaFix.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GigaFix.Daemons;
using GigaFix.Data;
using GigaFix.ViewModels.Dialogs;
using GigaFix.ViewModels.MainViews;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Splat.Microsoft.Extensions.DependencyInjection;

namespace GigaFix
{
    public class AppBootstrapper
    {
        public static IServiceProvider RegisterAll()
        {
            ServiceCollection services = new ServiceCollection();
            services.UseMicrosoftDependencyResolver();
            services.AddLogging(builder => builder.AddDebug().AddConsole());
            services.AddDbContext<AppDbContext>(ServiceLifetime.Transient);
            services.AddSingleton<NavigationService>();
            services.AddSingleton<AuthService>();
            services.AddSingleton<ApplicationsService>();
            services.AddSingleton<NotificationsService>();
            services.AddScoped<LoginViewModel>();
            services.AddScoped<RegisterViewModel>();
            services.AddScoped<AttachExecutorViewModel>();
            services.AddScoped<EditOrderViewModel>();
            services.AddSingleton<NotificationsViewModel>();
            services.AddScoped<OrdersListViewModel>();
            services.AddScoped<AddOrderViewModel>();
            services.AddTransient<StatisticViewModel>();
            services.AddTransient<MainViewModel>();
            services.AddScoped<MainWindowViewModel>();
            IServiceProvider container = services.BuildServiceProvider();
            container.UseMicrosoftDependencyResolver();
            return container;
        }
    }
}
