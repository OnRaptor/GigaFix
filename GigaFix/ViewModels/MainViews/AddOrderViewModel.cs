using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using GigaFix.Data;
using GigaFix.Services;
using SukiUI.Controls;

namespace GigaFix.ViewModels.MainViews;

public partial class AddOrderViewModel : PageViewModel
{
    public override string Title => "Добавить заявку";
    public override string IconName => "mdi-plus";

    [ObservableProperty] private IEnumerable<TypeEquipment> typeEquipments;
    [ObservableProperty] private IEnumerable<TypeProblem> typeProblems;
    [ObservableProperty] private IEnumerable<User> executors;
    [ObservableProperty] private int? number = null;
    [ObservableProperty] private int? cost = null;
    [ObservableProperty] private string equipmentName;
    [ObservableProperty] private string clientFio;
    [ObservableProperty] private string description;
    [ObservableProperty] private string status;
    [ObservableProperty] private TypeEquipment typeEquipment;
    [ObservableProperty] private TypeProblem typeProblem;
    [ObservableProperty] private User executor;

    private readonly ApplicationsService _applicationsService;

    public AddOrderViewModel(
        ApplicationsService applicationsService
    )
    {
        _applicationsService = applicationsService;
    }

    public override Action? OnNavigate => async () =>
    {
        TypeEquipments = await _applicationsService.GetEquipmentTypes();
        TypeProblems = await _applicationsService.GetProblemTypes();
        Executors = await _applicationsService.GetExecutors();
    };

    public async void AddApplication()
    {
        try
        {
            var r = await _applicationsService.CreateApplication(
                EquipmentName,
                Number.Value,
                Cost.Value,
                TypeEquipment.IdTypeEquipment,
                TypeEquipment.IdTypeEquipment,
                Status,
                ClientFio,
                Description,
                Executor.IdUser);
            await SukiHost.ShowToast("Результат", r);
        }
        catch (Exception e)
        {
            await SukiHost.ShowToast("Ошибка", e.Message);
        }
    }
}