using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using GigaFix.Data;
using Microsoft.EntityFrameworkCore;

namespace GigaFix.ViewModels.MainViews;

public partial class StatisticViewModel : PageViewModel
{
    public override string Title => "Статистика";
    public override string IconName => "mdi-chart-box";

    [ObservableProperty] private int completedApplicationsCount;
    [ObservableProperty] private TimeOnly avarageApplicationsWorkTime;
    public record StatItem(string Type, int Count, int CompletedCount,int WaitingCount,int WorkingCount, TimeSpan AvgTime);
    [ObservableProperty] private ObservableCollection<StatItem> statByType = new ();

    private readonly AppDbContext _dbContext;
    public StatisticViewModel(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override Action? OnNavigate => async () =>
    {
        //пришлось использовать ToListAsync, т.к FirstOrDefault генерирует говно запрос
        CompletedApplicationsCount = (await _dbContext.Database.SqlQueryRaw<int>(
                "select count(*) from application where status = 'выполнено';")
            .ToListAsync()).FirstOrDefault();
        AvarageApplicationsWorkTime = (await _dbContext.Database.SqlQueryRaw<TimeOnly>(
            "select sec_to_time(avg(time_to_sec(time_work))) as avgtime from application where status = 'выполнено';")
            .ToListAsync()).FirstOrDefault();
        
        using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = 
                @"select type_problem.name,
                    count(*),
                    count(if(application.status like ""выполнено"", 1, null)),
                    count(if(application.status like ""в ожидании"", 1, null)),
                    count(if(application.status like ""в работе"", 1, null)),
                    sec_to_time(avg(time_to_sec(application.time_work)))
                    from application
                join type_problem on application.id_type_problem = type_problem.id_type_problemt
                group by type_problem.name;";
            _dbContext.Database.OpenConnection();
            using (var result = command.ExecuteReader())
            {
                while (result.Read())
                {
                    StatByType.Add(new StatItem(
                        result.GetString(0),
                        result.GetInt32(1),
                        result.GetInt32(2),
                        result.GetInt32(3),
                        result.GetInt32(4),
                        (TimeSpan)result.GetValue(5)));
                }
            }
        }
    };
    
}