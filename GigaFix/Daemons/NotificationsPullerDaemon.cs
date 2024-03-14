using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;
using GigaFix.Services;
using GigaFix.ViewModels.MainViews;
using Microsoft.Extensions.Logging;
using SukiUI.Controls;

namespace GigaFix.Daemons;

public class NotificationsPullerDaemon
{
    //Должен запускаться в отдельном потоке
    public static async void Process(CancellationToken ct)
    {
        var logger = App.GetRequiredService<ILoggerFactory>().CreateLogger("NotificationsPullerDaemon");
        logger.LogInformation("Daemon started");
        var notificationsService = App.GetRequiredService<NotificationsService>();
        while (true)
        {
            if (ct.IsCancellationRequested)
                break;
            
            logger.LogInformation("Pulling new notifications");
            var message = await notificationsService.PullNewMessage();
            if (message != null)
            {
                await Dispatcher.UIThread.InvokeAsync(async () => await App.GetRequiredService<NotificationsViewModel>().PullLast());
                await Dispatcher.UIThread.InvokeAsync(() => SukiHost.ShowToast("Уведомление о заявке", message));
            }

            await Task.Delay(3000);
        }
        logger.LogInformation("Daemon stopped via cancellation");
    }
}