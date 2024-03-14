using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;
using GigaFix.Daemons;
using GigaFix.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SukiUI.Controls;

namespace GigaFix.Services;

public class AuthService
{
    public string AuthenticatedUserName { get; set; }
    public int AuthenticatedUserId { get; set; }
    public bool IsDispatcher { get; set; }
    
    private readonly AppDbContext _dbContext;
    private CancellationTokenSource _cancellationTokenSource;
    public AuthService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Login(string login, string password)
    {
        var res =  await _dbContext.Users
            .Where(u => u.Login == login && u.Password == password)
            .FirstOrDefaultAsync();
        if (res != null)
        {
            AuthenticatedUserId = res.IdUser;
            AuthenticatedUserName = res.FullnameUser ?? "Гость";
            AuthenticatedUserName = AuthenticatedUserName.Split(' ')[0] + " " 
                                    + string.Join(".", 
                AuthenticatedUserName.Split(' ')[1..]
                .Select(t => t[0]));
            App.GetRequiredService<ILogger<AuthService>>()
                .LogInformation($"User {AuthenticatedUserName} with role - {res.Role} logged in\n");
            
            IsDispatcher = res.Role == "Диспетчер";
            _cancellationTokenSource = new();
            if (IsDispatcher)
                await Task.Run(() => NotificationsPullerDaemon.Process(_cancellationTokenSource.Token));
            
            return true;
        }
        
        return false;
    }

    public void Logout()
    {
        _cancellationTokenSource.Cancel();
    }
}