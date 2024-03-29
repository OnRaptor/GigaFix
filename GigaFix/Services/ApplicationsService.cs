using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GigaFix.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GigaFix.Services;

public class ApplicationsService
{
    private readonly ILogger<ApplicationsService> _logger;
    private readonly AppDbContext _appDbContext;
    private readonly NotificationsService _notificationsService;

    public ApplicationsService(AppDbContext appDbContext, ILogger<ApplicationsService> logger,
        NotificationsService notificationsService)
    {
        _appDbContext = appDbContext;
        _logger = logger;
        _notificationsService = notificationsService;
    }

    public async Task<IEnumerable<Application>> GetApplications()
    {
        var applications = await _appDbContext
            .Applications
            .Include(o => o.IdTypeEquipmentNavigation)
            .Include(o => o.IdTypeProblemNavigation)
            .Include(o => o.IdEmployeeNavigation)
            .ToListAsync();
        _logger.LogInformation($"Found {applications.Count} applications");
        return applications;
    }

    public async Task<Application?> GetApplication(int applicationId)
    {
        return await _appDbContext.Applications.FindAsync(applicationId);
    }

    public async Task<IEnumerable<TypeEquipment>> GetEquipmentTypes()
    {
        return await _appDbContext.TypeEquipments.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<TypeProblem>> GetProblemTypes()
    {
        return await _appDbContext.TypeProblems.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<User>> GetExecutors()
    {
        return await _appDbContext.Users.AsNoTracking().Where(o => o.Role.ToLower() == "исполнитель").ToListAsync();
    }

    public async Task<string> CreateApplication(
        string equipment,
        int number,
        int cost,
        int equipmentTypeId,
        int problemTypeId,
        string status,
        string clientFio,
        string description,
        int employeeId)
    {
        var application = new Application
        {
            NameEquipment = equipment,
            IdTypeEquipment = equipmentTypeId,
            IdTypeProblem = problemTypeId,
            Status = status,
            WorkStatus = "Не выполнено",
            NameClient = clientFio,
            Description = description,
            IdEmployee = employeeId,
            DateAddition = DateOnly.FromDateTime(DateTime.Now),
            Number = number,
            Cost = cost
        };
        try
        {
            _appDbContext.Applications.Add(application);
            await _appDbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return "Не удалось создать заявку: " + e.InnerException?.Message;
        }

        return "Заявка создана";
    }

    public async Task<string> EditApplication(
        int applicationId,
        string? comment,
        string? description,
        string? status,
        string? workStatus,
        TimeSpan? workTime,
        User? executor
    )
    {
        var t = await GetApplication(applicationId);
        if (t == null)
            return "Заявка не найдена";

        if (t.WorkStatus != workStatus)
            await _notificationsService.PushNotification(
                $"Статус заявки [{t.IdApplication}] изменен с [{t.WorkStatus}] на [{workStatus}]");

        var application = _appDbContext.Applications.Update(t);
        application.Entity.Comment = comment ?? application.Entity.Comment;
        application.Entity.Description = description ?? application.Entity.Description;
        application.Entity.Status = status ?? application.Entity.Status;
        application.Entity.WorkStatus = workStatus ?? application.Entity.WorkStatus;
        if (workTime != null)
            application.Entity.TimeWork = TimeOnly.FromTimeSpan(workTime.Value);
        if (executor != null)
            application.Entity.IdEmployee = executor.IdUser;
        if (workStatus == "Выполнено")
            application.Entity.DateEnd = DateOnly.FromDateTime(DateTime.Now);
        await _appDbContext.SaveChangesAsync();
        return "Заявка обновлена";
    }
}