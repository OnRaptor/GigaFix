using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using GigaFix.Data;
using Microsoft.EntityFrameworkCore;

namespace GigaFix.ViewModels.MainViews;

public partial class StatisticViewModel : PageViewModel
{
    public override string Title => "Статистика";
    public override string IconName => "mdi-chart-box";

    [ObservableProperty] private int completedApplicationsCount;
    [ObservableProperty] private int avarageApplicationsWorkTime;
    public record StatItem(string type, int count, TimeOnly avgtime);

    [ObservableProperty] private ObservableCollection<StatItem> statByType;


    private readonly AppDbContext _dbContext;
    public StatisticViewModel(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override Action? OnNavigate => async () =>
    {
        CompletedApplicationsCount  = await _dbContext.Database.SqlQueryRaw<int>("select count(*) from application where status = 'выполнено';").FirstOrDefaultAsync();
        AvarageApplicationsWorkTime = await _dbContext.Database.SqlQueryRaw<int>(
            "select * from application;\nselect sec_to_time(avg(time_to_sec(time_work))) as avgtime from application where status = 'выполнено';").FirstOrDefaultAsync();
    };
    
}