using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GigaFix.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GigaFix.Services;

public class NotificationsService
{
    private DateTime _currentTimeStamp = DateTime.UtcNow;

    private readonly ILogger<NotificationsService> _logger;
    private readonly AppDbContext _dbContext;
    private readonly AuthService _authService;

    public NotificationsService(AppDbContext dbContext, AuthService authService, ILogger<NotificationsService> logger)
    {
        _dbContext = dbContext;
        _authService = authService;
        _logger = logger;
    }

    public async Task<List<Tuple<int, string, DateTime>>> GetNotificationsForCurrentUser()
    {
        if (_authService.AuthenticatedUserId == null)
            return null;
        _logger.LogInformation("Retrieving notifications for user => " + _authService.AuthenticatedUserName);
        return await _dbContext.Notifications
            .Where(n => n.UserId == _authService.AuthenticatedUserId)
            .Select(n => new Tuple<int, string, DateTime>(n.Id, n.Message, n.Timestamp)).ToListAsync();
    }

    public async Task PushNotification(string message)
    {
        var userIds = _dbContext.Users.AsNoTracking().Distinct().ToList().Select(x => x.IdUser);
        //АХАХАХАХ это работает, сигма мув
        await Parallel.ForEachAsync(userIds, async (userId, ct) =>
        {
            var dbContextInstance = App.GetRequiredService<AppDbContext>();
            await dbContextInstance.Notifications.AddAsync(new Notification
            {
                Message = message, UserId = userId, Timestamp = DateTime.UtcNow
            }, ct);
            await dbContextInstance.SaveChangesAsync(ct);
        });

        _logger.LogInformation("Pushed notifications => " + message);
    }

    public async Task DeleteNotificationForCurrentUser(int id)
    {
        await _dbContext.Notifications
            .Where(n =>
                n.UserId == _authService.AuthenticatedUserId && n.Id == id)
            .ExecuteDeleteAsync();
        _logger.LogInformation("Deleted notification => " + id);
    }

    /// <summary>
    /// Возвращает новое уведомление для пользователя
    /// </summary>
    /// <returns></returns>
    public async Task<string?> PullNewMessage()
    {
        var lastMessage = (await GetNotificationsForCurrentUser()).TakeLast(1).FirstOrDefault();
        if (lastMessage?.Item3 > _currentTimeStamp)
        {
            _currentTimeStamp = lastMessage.Item3;
            return lastMessage?.Item2;
        }

        return null;
    }
}