using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using DynamicData;
using GigaFix.Services;
using Microsoft.Extensions.Logging;

namespace GigaFix.ViewModels.MainViews;
public record NotifItem(int id, string message, DateTime time);

public partial class NotificationsViewModel : PageViewModel
{
    public override string Title => "Уведомления";
    public override string IconName => "mdi-bell";
    [ObservableProperty]private ObservableCollection<NotifItem> notifications = new();
    
    
    private readonly NotificationsService _notificationsService;
    public NotificationsViewModel(NotificationsService notificationsService)
    {
        _notificationsService = notificationsService;
        App.GetRequiredService<ILogger<NotificationsViewModel>>().LogInformation("NotificationsViewModel created");
    }

    public override Action? OnNavigate => async () => {
        Notifications.Clear();
        var _notifications = await _notificationsService.GetNotificationsForCurrentUser();
        _notifications.Reverse();
        Notifications.AddRange(_notifications.Select(n => new NotifItem(n.Item1, n.Item2, n.Item3)));
    };

    public async Task PullLast()
    {
        Notifications.AddOrInsertRange(
            (await _notificationsService.GetNotificationsForCurrentUser())
            .TakeLast(1).Select(n => new NotifItem(n.Item1, n.Item2, n.Item3)),0);
    }

    public async void DeleteAll()
    {
        foreach (var notification in Notifications)
            await _notificationsService.DeleteNotificationForCurrentUser(notification.id);
        Notifications.Clear();
    }

    public async void Delete(int id)
    {
        await _notificationsService.DeleteNotificationForCurrentUser(id);
        Notifications.Remove(Notifications.First(n => n.id == id));
    }
}