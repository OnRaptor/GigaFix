using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using GigaFix.Data;
using GigaFix.Services;
using GigaFix.ViewModels.MainViews;
using SukiUI.Controls;

namespace GigaFix.ViewModels.Dialogs;

public partial class EditOrderViewModel : ViewModelBase
{
    [ObservableProperty] private string? description;
    [ObservableProperty] private string? comment;
    [ObservableProperty] private string? status;
    [ObservableProperty] private TimeSpan? workTime;
    [ObservableProperty] private User? executor;
    [ObservableProperty] private IEnumerable<User>? executors;

    public bool IsDispatcher => _authService.IsDispatcher;
    public bool IsExecutor => !_authService.IsDispatcher;
    
    private int _applicationId;
    private readonly AuthService _authService;
    private readonly ApplicationsService _applicationsService;
    public EditOrderViewModel(AuthService authService, ApplicationsService applicationsService)
    {
        _authService = authService;
        _applicationsService = applicationsService;
    }

    public async void Init(int applicationId)
    {
        _applicationId = applicationId;
        var application = await _applicationsService.GetApplication(_applicationId);
        Description = application.Description;
        Comment = application?.Comment[0].ToString().ToUpper() + application?.Comment.Substring(1);
        Status = application?.Status;
        Executors = await _applicationsService.GetExecutors();
    }
    public async void Save()
    {
        var res = await _applicationsService.EditApplication(
            _applicationId,
            Comment,
            Description,
            Status,
            WorkTime,
            Executor);
        SukiHost.CloseDialog();
        await SukiHost.ShowToast("Результат", res);
        App.GetRequiredService<OrdersListViewModel>().OnNavigate();
    }
}