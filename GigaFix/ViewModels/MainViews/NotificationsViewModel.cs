using System;
using System.Collections.ObjectModel;
using System.Linq;
using DynamicData;
using GigaFix.Services;

namespace GigaFix.ViewModels.MainViews;
public record NotifItem(int id, string message);

public class NotificationsViewModel : PageViewModel
{
    public override string Title => "Уведомления";
    public override string IconName => "mdi-bell";
    public ObservableCollection<NotifItem> Notifications { get; } = new();
    
    
    private readonly NotificationsService _notificationsService;
    public NotificationsViewModel(NotificationsService notificationsService)
    {
        _notificationsService = notificationsService;
    }

    public override Action? OnNavigate => async () => {
        Notifications.Clear();
        var notifications = await _notificationsService.GetNotificationsForCurrentUser();
        notifications.Reverse();
        Notifications.AddRange(notifications.Select(n => new NotifItem(n.Item1, n.Item2)));
    };

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