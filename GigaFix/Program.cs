using Avalonia;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Projektanker.Icons.Avalonia;
using Projektanker.Icons.Avalonia.MaterialDesign;
using SukiUI.Controls;

namespace GigaFix;

internal sealed class Program
{
    private static void ExceptionHandler(string message, Exception ex)
    {
        SukiHost.ShowToast("Непредвиденная ошибка", message);
        App.GetRequiredService<ILogger<Program>>().LogCritical(ex, "Unhandled exception");
    }

    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        AppBootstrapper.RegisterAll();

        //далее перехватываются вообще все возможные исключения
        #if RELEASE
        AppDomain.CurrentDomain
            .UnhandledException += (s, e) =>
        {
            var ex = e.ExceptionObject as Exception;
            ExceptionHandler(ex?.Message ?? "Неизвестная ошибка", ex);
        };
        TaskScheduler
            .UnobservedTaskException += (s, e) =>
        {
            ExceptionHandler(e.Exception.Message, e.Exception);
        };
        try
        {
#endif
            var app = BuildAvaloniaApp();
            app.StartWithClassicDesktopLifetime(args);
#if RELEASE
        }
        catch (Exception ex)
        {
            ExceptionHandler(ex.Message, ex);
        }
#endif
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
    {
        IconProvider.Current
            .Register<MaterialDesignIconProvider>();

        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
    }
}