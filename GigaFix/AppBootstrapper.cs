using GigaFix.Services;
using GigaFix.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GigaFix.Data;
using GigaFix.ViewModels.Dialogs;
using GigaFix.ViewModels.MainViews;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Splat.Microsoft.Extensions.DependencyInjection;

namespace GigaFix
{
    public class AppBootstrapper
    {
        public static void RegisterAll()
        {
            ServiceCollection services = new ServiceCollection();
            services.UseMicrosoftDependencyResolver();
            services.AddDbContext<AppDbContext>(opts =>
                opts.UseMySql(
                    ServerVersion.AutoDetect("host=localhost;port=3306;user=root;password=root;database=demo_ekz")
                    ), ServiceLifetime.Singleton);
            services.AddSingleton(new NavigationService());
            services.AddScoped<LoginViewModel>();
            services.AddScoped<RegisterViewModel>();
            services.AddScoped<AttachExecutorViewModel>();
            services.AddScoped<EditOrderViewModel>();
            services.AddScoped<NotificationsViewModel>();
            services.AddScoped<OrdersListViewModel>();
            services.AddScoped<AddOrderViewModel>();
            services.AddScoped<StatisticViewModel>();
            services.AddScoped<MainViewModel>();
            services.AddScoped<MainWindowViewModel>();
            IServiceProvider container = services.BuildServiceProvider();
            container.UseMicrosoftDependencyResolver();
        }
    }
}
